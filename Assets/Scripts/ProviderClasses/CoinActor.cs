using LeopotamGroup.Globals;

namespace HalfDiggers.Runner
{
    public class CoinActor : Actor
    {
        private readonly IPoolService _poolService;

        public CoinActor()
        {
            _poolService = Service<IPoolService>.Get();
        }
        public override void Handle()
        {
            ReturnToPool();
        }

        private void ReturnToPool()
        {
            _poolService.Return(gameObject);
        }
    }
}