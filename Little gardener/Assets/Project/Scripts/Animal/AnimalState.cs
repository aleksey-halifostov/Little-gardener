namespace LittleGardener.Animal
{
    public abstract class AnimalState
    {
        protected AnimalController _controller { get; }

        protected AnimalState(AnimalController controller)
        {
            _controller = controller;
        }

        public abstract void Do();
    }
}
