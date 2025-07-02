namespace LittleGardener.Animal
{
    public abstract class AnimalState
    {
        protected AnimalController _controller { get; }

        public AnimalState(AnimalController controller)
        {
            if (controller == null)
                throw new System.ArgumentNullException(nameof(controller));

            _controller = controller;
        }

        public abstract void Enter();
        public abstract void Do();
        public abstract void Exit();
    }
}
