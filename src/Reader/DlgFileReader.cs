using DlgToTxt.Model;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DlgToTxt.Reader
{
    public class DlgFileReader
    {
        public TlkFile TlkFile { get; set; }
        public DlgFile DlgFile { get; set; }

        public DlgFile Read(string filename)
        {
            using (FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                var f = Read(fs);
                f.Filename = Path.GetFileName(filename);
                return f;
            }
        }

        public DlgFile Read(Stream s)
        {
            using (BinaryReader br = new BinaryReader(s))
            {
                var dlgFile = ParseFile(br);
                return dlgFile;
            }
        }

        private DlgFile ParseFile(BinaryReader br)
        {
            var header = (DlgHeaderBinary)Common.ReadStruct(br, typeof(DlgHeaderBinary));

            if (Common.TryGetString(header.ftype) != "DLG ")
                return new DlgFile();

            List<StateBinary> states = new List<StateBinary>();
            List<TransitionBinary> transitions = new List<TransitionBinary>();
            List<StateTriggerBinary> stateTriggers = new List<StateTriggerBinary>();
            List<TransitionTriggerBinary> transitionTriggers = new List<TransitionTriggerBinary>();
            List<ActionTableBinary> actions = new List<ActionTableBinary>();

            br.BaseStream.Seek(header.StateOffset, SeekOrigin.Begin);
            for (int i = 0; i < header.StateCount; i++)
            {
                var state = (StateBinary)Common.ReadStruct(br, typeof(StateBinary));
                states.Add(state);
            }

            br.BaseStream.Seek(header.TransitionOffset, SeekOrigin.Begin);
            for (int i = 0; i < header.TransitionCount; i++)
            {
                var transition = (TransitionBinary)Common.ReadStruct(br, typeof(TransitionBinary));
                transitions.Add(transition);
            }

            br.BaseStream.Seek(header.StateTriggerOffset, SeekOrigin.Begin);
            for (int i = 0; i < header.StateTriggerCount; i++)
            {
                var stateTrigger = (StateTriggerBinary)Common.ReadStruct(br, typeof(StateTriggerBinary));
                stateTriggers.Add(stateTrigger);
            }

            br.BaseStream.Seek(header.TransitionTriggerOffset, SeekOrigin.Begin);
            for (int i = 0; i < header.TransitionTriggerCount; i++)
            {
                var transitionTrigger = (TransitionTriggerBinary)Common.ReadStruct(br, typeof(TransitionTriggerBinary));
                transitionTriggers.Add(transitionTrigger);
            }

            br.BaseStream.Seek(header.ActionTableOffset, SeekOrigin.Begin);
            for (int i = 0; i < header.ActionTableCount; i++)
            {
                var action = (ActionTableBinary)Common.ReadStruct(br, typeof(ActionTableBinary));
                actions.Add(action);
            }

            DlgFile dlgFile = new DlgFile();
            dlgFile.Flags.Enemy = (header.Flags & Common.Bit0) != 0;
            dlgFile.Flags.EscapeArea = (header.Flags & Common.Bit1) != 0;
            dlgFile.Flags.Nothing = (header.Flags & Common.Bit2) != 0;
            dlgFile.Flags.Bit03 = (header.Flags & Common.Bit3) != 0;
            dlgFile.Flags.Bit04 = (header.Flags & Common.Bit4) != 0;
            dlgFile.Flags.Bit05 = (header.Flags & Common.Bit5) != 0;
            dlgFile.Flags.Bit06 = (header.Flags & Common.Bit6) != 0;
            dlgFile.Flags.Bit07 = (header.Flags & Common.Bit7) != 0;
            dlgFile.Flags.Bit08 = (header.Flags & Common.Bit8) != 0;
            dlgFile.Flags.Bit09 = (header.Flags & Common.Bit9) != 0;
            dlgFile.Flags.Bit10 = (header.Flags & Common.Bit10) != 0;
            dlgFile.Flags.Bit11 = (header.Flags & Common.Bit11) != 0;
            dlgFile.Flags.Bit12 = (header.Flags & Common.Bit12) != 0;
            dlgFile.Flags.Bit13 = (header.Flags & Common.Bit13) != 0;
            dlgFile.Flags.Bit14 = (header.Flags & Common.Bit14) != 0;
            dlgFile.Flags.Bit15 = (header.Flags & Common.Bit15) != 0;
            dlgFile.Flags.Bit16 = (header.Flags & Common.Bit16) != 0;
            dlgFile.Flags.Bit17 = (header.Flags & Common.Bit17) != 0;
            dlgFile.Flags.Bit18 = (header.Flags & Common.Bit18) != 0;
            dlgFile.Flags.Bit19 = (header.Flags & Common.Bit19) != 0;
            dlgFile.Flags.Bit20 = (header.Flags & Common.Bit20) != 0;
            dlgFile.Flags.Bit21 = (header.Flags & Common.Bit21) != 0;
            dlgFile.Flags.Bit22 = (header.Flags & Common.Bit22) != 0;
            dlgFile.Flags.Bit23 = (header.Flags & Common.Bit23) != 0;
            dlgFile.Flags.Bit24 = (header.Flags & Common.Bit24) != 0;
            dlgFile.Flags.Bit25 = (header.Flags & Common.Bit25) != 0;
            dlgFile.Flags.Bit26 = (header.Flags & Common.Bit26) != 0;
            dlgFile.Flags.Bit27 = (header.Flags & Common.Bit27) != 0;
            dlgFile.Flags.Bit28 = (header.Flags & Common.Bit28) != 0;
            dlgFile.Flags.Bit29 = (header.Flags & Common.Bit29) != 0;
            dlgFile.Flags.Bit30 = (header.Flags & Common.Bit30) != 0;
            dlgFile.Flags.Bit31 = (header.Flags & Common.Bit31) != 0;


            UTF8Encoding enc = new UTF8Encoding();

            var numberOfStatesWithTriggers = 0;
            var StateNumber = 0;
            foreach (var state in states)
            {
                State2 state2 = new State2();
                state2.ResponseText = Common.ReadString(state.ResponseText, TlkFile);
                state2.StateNumber = StateNumber;
                StateNumber++;

                for (int i = 0; i < state.TransitionCount; i++)
                {
                    var transition2 = new Transition2();

                    if ((transitions[state.TransitionIndex + i].Flags & Common.Bit0) != 0)
                    {
                        transition2.HasText = true;
                        transition2.TransitionText = Common.ReadString(transitions[state.TransitionIndex + i].TransitionText, TlkFile);
                    }

                    if ((transitions[state.TransitionIndex + i].Flags & Common.Bit1) != 0)
                    {
                        transition2.HasTrigger = true;
                        br.BaseStream.Seek(transitionTriggers[transitions[state.TransitionIndex + i].TransitionTrigger].TransitionTriggerStringOffset, SeekOrigin.Begin);
                        var transitionTriggerBuffer = new byte[transitionTriggers[transitions[state.TransitionIndex + i].TransitionTrigger].TransitionTriggerStringLength];
                        br.BaseStream.Read(transitionTriggerBuffer, 0, transitionTriggers[transitions[state.TransitionIndex + i].TransitionTrigger].TransitionTriggerStringLength);
                        var transitionTriggerData = enc.GetString(transitionTriggerBuffer);
                        transition2.Triggers.AddRange(transitionTriggerData.Split(new string[] { "\r\n" }, System.StringSplitOptions.RemoveEmptyEntries));
                    }

                    if ((transitions[state.TransitionIndex + i].Flags & Common.Bit2) != 0)
                    {
                        transition2.HasAction = true;
                        br.BaseStream.Seek(actions[transitions[state.TransitionIndex + i].ActionIndex].ActionTableStringOffset, SeekOrigin.Begin);
                        var actionBuffer = new byte[actions[transitions[state.TransitionIndex + i].ActionIndex].ActionTableStringLength];
                        br.BaseStream.Read(actionBuffer, 0, actions[transitions[state.TransitionIndex + i].ActionIndex].ActionTableStringLength);
                        var actionData = enc.GetString(actionBuffer);
                        transition2.Actions.AddRange(actionData.Split(new string[] { "\r\n" }, System.StringSplitOptions.RemoveEmptyEntries));
                    }

                    transition2.TerminateDialog = true;
                    if ((transitions[state.TransitionIndex + i].Flags & Common.Bit3) == 0)
                    {
                        transition2.TerminateDialog = false;
                        transition2.NextState = transitions[state.TransitionIndex + i].NextState;
                        transition2.Dialog = Common.TryGetString(transitions[state.TransitionIndex + i].Dialog);
                    }

                    if ((transitions[state.TransitionIndex + i].Flags & Common.Bit4) != 0)
                    {
                        transition2.HasJournal = true;
                        transition2.JournalText = Common.ReadString(transitions[state.TransitionIndex + i].JournalText, TlkFile);
                    }

                    transition2.Unknown = (transitions[state.TransitionIndex + i].Flags & Common.Bit5) != 0;
                    transition2.AddQuestJournalEntry = (transitions[state.TransitionIndex + i].Flags & Common.Bit6) != 0;
                    transition2.RemoveQuestJournalEntry = (transitions[state.TransitionIndex + i].Flags & Common.Bit7) != 0;
                    transition2.AddQuestCompleteJournalEntry = (transitions[state.TransitionIndex + i].Flags & Common.Bit8) != 0;

                    state2.Transitions.Add(transition2);
                }

                if (state.TriggerIndex != -1)
                {
                    br.BaseStream.Seek(stateTriggers[state.TriggerIndex].StateTriggerStringOffset, SeekOrigin.Begin);
                    var stateTriggerBuffer = new byte[stateTriggers[state.TriggerIndex].StateTriggerStringLength];
                    br.BaseStream.Read(stateTriggerBuffer, 0, stateTriggers[state.TriggerIndex].StateTriggerStringLength);
                    var stateTriggerData = enc.GetString(stateTriggerBuffer);
                    state2.Triggers.AddRange(stateTriggerData.Split(new string[] { "\r\n" }, System.StringSplitOptions.RemoveEmptyEntries));

                    if (state.TriggerIndex != numberOfStatesWithTriggers)
                    {
                        state2.Weight = state.TriggerIndex;
                    }
                    numberOfStatesWithTriggers++;
                }

                dlgFile.States.Add(state2);
            }

            return dlgFile;
        }
    }
}