namespace bUtility.TokenCache.Data {
	
	public partial class TokenReplayCache_Base : PersistentLib.BaseTable, ITokenReplayCache {
		protected System.Nullable<System.Guid> _p_id;
		protected string _p_tokenkey;
		protected System.Nullable<System.DateTime> _p_expirationtime;
		protected byte[] _p_securitytoken;
		public virtual System.Nullable<System.Guid> _Id {
			get {
				return this._p_id;
			}
			set {
				this._p_id = value;
			}
		}
		public virtual string _TokenKey {
			get {
				return this._p_tokenkey;
			}
			set {
				this._p_tokenkey = value;
			}
		}
		public virtual System.Nullable<System.DateTime> _ExpirationTime {
			get {
				return this._p_expirationtime;
			}
			set {
				this._p_expirationtime = value;
			}
		}
		public virtual byte[] _SecurityToken {
			get {
				return this._p_securitytoken;
			}
			set {
				this._p_securitytoken = value;
			}
		}
		protected virtual void InternalCopy(TokenReplayCache_Base existingobject) {
			if ((existingobject == null)) {
				return;
			}
			this._p_id = existingobject._p_id;
			this._p_tokenkey = existingobject._p_tokenkey;
			this._p_expirationtime = existingobject._p_expirationtime;
			this._p_securitytoken = existingobject._p_securitytoken;
		}
		public virtual void Copy(ITokenReplayCache existingobject) {
			if ((existingobject == null)) {
				return;
			}
			this._Id = existingobject._Id;
			this._TokenKey = existingobject._TokenKey;
			this._ExpirationTime = existingobject._ExpirationTime;
			this._SecurityToken = existingobject._SecurityToken;
		}
		public virtual bool IsEqual(ITokenReplayCache existingobject) {
			if ((existingobject == null)) {
				return false;
			}
			if ((this._Id != existingobject._Id)) {
				return false;
			}
			if ((this._TokenKey != existingobject._TokenKey)) {
				return false;
			}
			if ((this._ExpirationTime != existingobject._ExpirationTime)) {
				return false;
			}
			if ((this._SecurityToken != existingobject._SecurityToken)) {
				return false;
			}
			return true;
		}
		public enum Fields {
			_Id,
			_TokenKey,
			_ExpirationTime,
			_SecurityToken,
		}
	}
}
