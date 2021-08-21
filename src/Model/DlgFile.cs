using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using static DlgToTxt.Common;

namespace DlgToTxt.Model
{
    public class StateCollection : Collection<State2>
    {
        public State2 StateNumber(int stateNumber)
        {
            return this.Where(a => a.StateNumber == stateNumber).SingleOrDefault();
        }
    }

    public class DlgFile
    {
        public StateCollection States = new StateCollection();

        private string filename;
        public string Filename { get { return filename; } set { filename = value; } }
        public HeaderFlags Flags;
    }

    public class State2
    {
        public State2()
        {
            Triggers = new List<string>();
        }

        public Int32 Weight;
        public Int32 StateNumber;
        public List<string> Triggers;
        public IEString ResponseText;
        public string SymbolicName;
        public List<Transition2> Transitions = new List<Transition2>();
    }

    public class Transition2
    {
        public Transition2()
        {
            Actions = new List<string>();
            Triggers = new List<string>();
        }

        public List<string> Triggers;
        public IEString TransitionText;
        public IEString JournalText;
        public string Dialog;
        public Int32 NextState;
        public string NextStateSymbolicName;
        public List<string> Actions;
        public bool HasText { get; set; }
        public bool HasTrigger { get; set; }
        public bool HasAction { get; set; }
        public bool TerminateDialog { get; set; }
        public bool HasJournal { get; set; }
        public bool Unknown { get; set; }
        public bool AddQuestJournalEntry { get; set; }
        public bool RemoveQuestJournalEntry { get; set; }
        public bool AddQuestCompleteJournalEntry { get; set; }
    }

    public struct HeaderFlags
    {
        public bool Enemy { get; set; }
        public bool EscapeArea { get; set; }
        public bool Nothing { get; set; }
        public bool Bit03 { get; set; }
        public bool Bit04 { get; set; }
        public bool Bit05 { get; set; }
        public bool Bit06 { get; set; }
        public bool Bit07 { get; set; }
        public bool Bit08 { get; set; }
        public bool Bit09 { get; set; }
        public bool Bit10 { get; set; }
        public bool Bit11 { get; set; }
        public bool Bit12 { get; set; }
        public bool Bit13 { get; set; }
        public bool Bit14 { get; set; }
        public bool Bit15 { get; set; }
        public bool Bit16 { get; set; }
        public bool Bit17 { get; set; }
        public bool Bit18 { get; set; }
        public bool Bit19 { get; set; }
        public bool Bit20 { get; set; }
        public bool Bit21 { get; set; }
        public bool Bit22 { get; set; }
        public bool Bit23 { get; set; }
        public bool Bit24 { get; set; }
        public bool Bit25 { get; set; }
        public bool Bit26 { get; set; }
        public bool Bit27 { get; set; }
        public bool Bit28 { get; set; }
        public bool Bit29 { get; set; }
        public bool Bit30 { get; set; }
        public bool Bit31 { get; set; }
    }
}