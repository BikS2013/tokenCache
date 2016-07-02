namespace bUtility.TokenCache.Data {
	
	public partial class SecurityTokenCache_Base : PersistentLib.BaseTable, ISecurityTokenCache {
		protected System.Nullable<System.Guid> _p_id;
		protected string _p_contextid;
		protected string _p_endpointid;
		protected string _p_keygeneration;
		protected System.Nullable<System.DateTime> _p_expirytime;
		protected System.Nullable<System.DateTime> _p_rollingexpirytime;
		protected byte[] _p_sessionsecuritytokenvalue;
		protected string _p_sessionsecuritytokenid;
		protected string _p_username;
		public virtual System.Nullable<System.Guid> _Id {
			get {
				return this._p_id;
			}
			set {
				this._p_id = value;
			}
		}
		public virtual string _ContextID {
			get {
				return this._p_contextid;
			}
			set {
				this._p_contextid = value;
			}
		}
		public virtual string _EndpointID {
			get {
				return this._p_endpointid;
			}
			set {
				this._p_endpointid = value;
			}
		}
		public virtual string _KeyGeneration {
			get {
				return this._p_keygeneration;
			}
			set {
				this._p_keygeneration = value;
			}
		}
		public virtual System.Nullable<System.DateTime> _ExpiryTime {
			get {
				return this._p_expirytime;
			}
			set {
				this._p_expirytime = value;
			}
		}
		public virtual System.Nullable<System.DateTime> _RollingExpiryTime {
			get {
				return this._p_rollingexpirytime;
			}
			set {
				this._p_rollingexpirytime = value;
			}
		}
		public virtual byte[] _SessionSecurityTokenValue {
			get {
				return this._p_sessionsecuritytokenvalue;
			}
			set {
				this._p_sessionsecuritytokenvalue = value;
			}
		}
		public virtual string _SessionSecurityTokenID {
			get {
				return this._p_sessionsecuritytokenid;
			}
			set {
				this._p_sessionsecuritytokenid = value;
			}
		}
		public virtual string _UserName {
			get {
				return this._p_username;
			}
			set {
				this._p_username = value;
			}
		}
		protected virtual void InternalCopy(SecurityTokenCache_Base existingobject) {
			if ((existingobject == null)) {
				return;
			}
			this._p_id = existingobject._p_id;
			this._p_contextid = existingobject._p_contextid;
			this._p_endpointid = existingobject._p_endpointid;
			this._p_keygeneration = existingobject._p_keygeneration;
			this._p_expirytime = existingobject._p_expirytime;
			this._p_rollingexpirytime = existingobject._p_rollingexpirytime;
			this._p_sessionsecuritytokenvalue = existingobject._p_sessionsecuritytokenvalue;
			this._p_sessionsecuritytokenid = existingobject._p_sessionsecuritytokenid;
			this._p_username = existingobject._p_username;
		}
		public virtual void Copy(ISecurityTokenCache existingobject) {
			if ((existingobject == null)) {
				return;
			}
			this._Id = existingobject._Id;
			this._ContextID = existingobject._ContextID;
			this._EndpointID = existingobject._EndpointID;
			this._KeyGeneration = existingobject._KeyGeneration;
			this._ExpiryTime = existingobject._ExpiryTime;
			this._RollingExpiryTime = existingobject._RollingExpiryTime;
			this._SessionSecurityTokenValue = existingobject._SessionSecurityTokenValue;
			this._SessionSecurityTokenID = existingobject._SessionSecurityTokenID;
			this._UserName = existingobject._UserName;
		}
		public virtual bool IsEqual(ISecurityTokenCache existingobject) {
			if ((existingobject == null)) {
				return false;
			}
			if ((this._Id != existingobject._Id)) {
				return false;
			}
			if ((this._ContextID != existingobject._ContextID)) {
				return false;
			}
			if ((this._EndpointID != existingobject._EndpointID)) {
				return false;
			}
			if ((this._KeyGeneration != existingobject._KeyGeneration)) {
				return false;
			}
			if ((this._ExpiryTime != existingobject._ExpiryTime)) {
				return false;
			}
			if ((this._RollingExpiryTime != existingobject._RollingExpiryTime)) {
				return false;
			}
			if ((this._SessionSecurityTokenValue != existingobject._SessionSecurityTokenValue)) {
				return false;
			}
			if ((this._SessionSecurityTokenID != existingobject._SessionSecurityTokenID)) {
				return false;
			}
			if ((this._UserName != existingobject._UserName)) {
				return false;
			}
			return true;
		}
		public enum Fields {
			_Id,
			_ContextID,
			_EndpointID,
			_KeyGeneration,
			_ExpiryTime,
			_RollingExpiryTime,
			_SessionSecurityTokenValue,
			_SessionSecurityTokenID,
			_UserName,
		}
	}
}
