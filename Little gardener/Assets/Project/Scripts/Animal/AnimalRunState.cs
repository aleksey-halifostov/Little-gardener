using UnityEngine;
using LittleGardener.Spawn;

namespace LittleGardener.Animal
{
    public class AnimalRunState : AnimalState
    {
        private Transform _originalTransform;
        private int _runTrigger = Animator.StringToHash("Run");

        public AnimalRunState(AnimalController controller, Transform transform) : base(controller)
        {
            if (transform == null)
                throw new System.ArgumentNullException(nameof(transform));

            _originalTransform = transform;
        }

        public override void Enter()
        {
            _controller.SetAnimatorTrigger(_runTrigger);
        }

        public override void Do()
        {
            _originalTransform.Translate(_controller.CurrentDirection * _controller.Speed * Time.deltaTime);
            _controller.Sort();

            if (_originalTransform.position.x > SpawnManager.XSpawnBound || _originalTransform.position.x  < -SpawnManager.XSpawnBound ||
                _originalTransform.position.y > SpawnManager.YSpawnBound || _originalTransform.position.y < -SpawnManager.YSpawnBound)
            {
                GameObject.Destroy(_originalTransform.gameObject);
            }
        }

        public override void Exit() { }
    }
}
