using SabberStone.Actions;

namespace SabberStone.Tasks.SimpleTasks
{
    public class ManaCrystalFullTask : SimpleTask
    {
        public ManaCrystalFullTask(int amount)
        {
            Amount = amount;
        }

        public int Amount { get; set; }

        public override TaskState Process()
        {
            var success = Generic.ChangeManaCrystal.Invoke(Controller, Amount, true);
            return TaskState.COMPLETE;
        }

        public override ISimpleTask Clone()
        {
            var clone = new ManaCrystalFullTask(Amount);
            clone.Copy(this);
            return clone;
        }
    }
}