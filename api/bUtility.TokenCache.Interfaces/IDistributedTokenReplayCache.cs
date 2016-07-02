using bUtility.TokenCache.Types.Replay;

namespace bUtility.TokenCache.Interfaces
{
    public interface IDistributedTokenReplayCache
    {
        bool AddOrUpdate(ReplayCacheEntry request);


        bool Contains(string key);


        ReplayCacheEntry Get(string key);


        bool Remove(string key);

        bool Purge();

    }
}
