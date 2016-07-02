namespace bUtility.TokenCache.Data {
	
	public interface ISecurityTokenCache {
		System.Nullable<System.Guid> _Id {
			get;
			set;
		}
		string _ContextID {
			get;
			set;
		}
		string _EndpointID {
			get;
			set;
		}
		string _KeyGeneration {
			get;
			set;
		}
		System.Nullable<System.DateTime> _ExpiryTime {
			get;
			set;
		}
		System.Nullable<System.DateTime> _RollingExpiryTime {
			get;
			set;
		}
		byte[] _SessionSecurityTokenValue {
			get;
			set;
		}
		string _SessionSecurityTokenID {
			get;
			set;
		}
		string _UserName {
			get;
			set;
		}
		void Copy(ISecurityTokenCache existingobject);
		bool IsEqual(ISecurityTokenCache existingobject);
	}
}
