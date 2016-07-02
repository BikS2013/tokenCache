namespace bUtility.TokenCache.Data {
	
	public interface ITokenReplayCache {
		System.Nullable<System.Guid> _Id {
			get;
			set;
		}
		string _TokenKey {
			get;
			set;
		}
		System.Nullable<System.DateTime> _ExpirationTime {
			get;
			set;
		}
		byte[] _SecurityToken {
			get;
			set;
		}
		void Copy(ITokenReplayCache existingobject);
		bool IsEqual(ITokenReplayCache existingobject);
	}
}
