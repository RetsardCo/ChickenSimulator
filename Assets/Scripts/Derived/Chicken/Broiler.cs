

namespace com.nullproject.project1
{
    public sealed class Broiler : Chicken
    {
        public override void GenerateEgg(int pendingEggsToLay)
        {
            print($"{petName} cannot generate egg: Classified as Broiler");
        }
    }
}