namespace bUtility.TokenCache.Data {
	using System;
	using System.Text;
	using System.Diagnostics;
	using System.Data;
	using System.Xml.Serialization;
	using Newtonsoft.Json;
	using PersistentLib;
	
	[Serializable()]
	[Table("TokenReplayCache")]
	public partial class TokenReplayCache : PersistentLib.PersistentTable, ITokenReplayCache {
		protected System.Nullable<System.Guid> _p_id;
		protected string _p_tokenkey;
		protected System.Nullable<System.DateTime> _p_expirationtime;
		protected byte[] _p_securitytoken;
		protected static string DefaultInsertQuery = "Insert into TokenReplayCache ( Id, TokenKey, ExpirationTime, SecurityToken) Value" +
			"s ( @Id, @TokenKey, @ExpirationTime, @SecurityToken )";
		protected static string DefaultUpdateSetQuery = "Id = @Id, TokenKey = @TokenKey, ExpirationTime = @ExpirationTime, SecurityToken =" +
			" @SecurityToken";
		[Column("Id", typeof(System.Nullable<System.Guid>), false)]
		[Parameter("@Id", System.Data.SqlDbType.UniqueIdentifier, "Id")]
		public virtual System.Nullable<System.Guid> _Id {
			get {
				return this._p_id;
			}
			set {
				this._p_id = value;
			}
		}
		[Column("TokenKey", typeof(string), 100)]
		[Parameter("@TokenKey", System.Data.SqlDbType.VarChar, 100, "TokenKey")]
		public virtual string _TokenKey {
			get {
				return this._p_tokenkey;
			}
			set {
				this._p_tokenkey = value;
			}
		}
		[Column("ExpirationTime", typeof(System.Nullable<System.DateTime>), false)]
		[Parameter("@ExpirationTime", System.Data.SqlDbType.DateTime, "ExpirationTime")]
		public virtual System.Nullable<System.DateTime> _ExpirationTime {
			get {
				return this._p_expirationtime;
			}
			set {
				this._p_expirationtime = value;
			}
		}
		[Column("SecurityToken", typeof(byte[]), false)]
		[Parameter("@SecurityToken", System.Data.SqlDbType.VarBinary, "SecurityToken")]
		public virtual byte[] _SecurityToken {
			get {
				return this._p_securitytoken;
			}
			set {
				this._p_securitytoken = value;
			}
		}
		public override string PhysicalTableName {
			get {
				return "TokenReplayCache";
			}
		}
		public override string InsertCommandText {
			get {
				return DefaultInsertQuery;
			}
		}
		public override string UpdateCommandSetText {
			get {
				return DefaultUpdateSetQuery;
			}
		}
		protected virtual void InternalCopy(TokenReplayCache existingobject) {
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
		public override void ParseRow(System.Data.DataRow row) {
			_Id = ((System.Nullable<System.Guid>)(GetRowValue(row, "Id")));
			_TokenKey = ((string)(GetRowValue(row, "TokenKey")));
			_ExpirationTime = ((System.Nullable<System.DateTime>)(GetRowValue(row, "ExpirationTime")));
			_SecurityToken = ((byte[])(GetRowValue(row, "SecurityToken")));
		}
		public override void ReadReader(PersistentLib.PersistentObject item, PersistentLib.ReaderWrapper reader) {
			((TokenReplayCache)(item))._Id = ((System.Nullable<System.Guid>)(reader.GetField("ID")));
			((TokenReplayCache)(item))._TokenKey = ((string)(reader.GetField("TOKENKEY")));
			((TokenReplayCache)(item))._ExpirationTime = ((System.Nullable<System.DateTime>)(reader.GetField("EXPIRATIONTIME")));
			((TokenReplayCache)(item))._SecurityToken = ((byte[])(reader.GetField("SECURITYTOKEN")));
		}
		public override System.Data.DataRow ToRow(System.Data.DataRow row) {
			this.BeforeToRow(row);
			SetRowValue(row, "Id", this._Id);
			SetRowValue(row, "TokenKey", this._TokenKey);
			SetRowValue(row, "ExpirationTime", this._ExpirationTime);
			SetRowValue(row, "SecurityToken", this._SecurityToken);
			this.AfterToRow(row);
			return row;
		}
		public override void BuildDataParams() {
			this.DataParams = new PersistentLib.SqlParamDictionary();
			System.Data.IDbDataParameter p_Id = this.SqlFactory.CreateParam("@Id", System.Data.SqlDbType.UniqueIdentifier);
			p_Id.SourceColumn = "Id";
			this.DataParams.Add("@Id", p_Id);
			System.Data.IDbDataParameter p_TokenKey = this.SqlFactory.CreateParam("@TokenKey", System.Data.SqlDbType.VarChar);
			p_TokenKey.SourceColumn = "TokenKey";
			this.DataParams.Add("@TokenKey", p_TokenKey);
			System.Data.IDbDataParameter p_ExpirationTime = this.SqlFactory.CreateParam("@ExpirationTime", System.Data.SqlDbType.DateTime);
			p_ExpirationTime.SourceColumn = "ExpirationTime";
			this.DataParams.Add("@ExpirationTime", p_ExpirationTime);
			System.Data.IDbDataParameter p_SecurityToken = this.SqlFactory.CreateParam("@SecurityToken", System.Data.SqlDbType.VarBinary);
			p_SecurityToken.SourceColumn = "SecurityToken";
			this.DataParams.Add("@SecurityToken", p_SecurityToken);
		}
		public override void UpdateDataParameters(PersistentLib.SqlParamDictionary paramlist) {
			SetParameterValue(paramlist, "@Id", this._Id);
			SetParameterValue(paramlist, "@TokenKey", this._TokenKey);
			SetParameterValue(paramlist, "@ExpirationTime", this._ExpirationTime);
			SetParameterValue(paramlist, "@SecurityToken", this._SecurityToken);
		}
		public virtual void Copy(TokenReplayCache existingobject) {
			this.InitializeObject(existingobject);
			this.Copy(((ITokenReplayCache)(existingobject)));
		}
		public virtual TokenReplayCache Copy() {
			TokenReplayCache newobject = new TokenReplayCache();
			newobject.Copy(this);
			return newobject;
		}
		public virtual T Copy<T>()
			where T : TokenReplayCache, new () {
			T v = new T();
			v.Copy(this);
			return v;
		}
		private System.Xml.Serialization.XmlSerializer GetXmlSerializer() {
			return new System.Xml.Serialization.XmlSerializer(typeof(TokenReplayCache));
		}
		public override void CopyDataFrom(PersistentLib.PersistentObject existing) {
			this.Copy(((ITokenReplayCache)(existing)));
		}
		public override PersistentLib.PersistentObject GetCopy() {
			TokenReplayCache item = new TokenReplayCache();
			item.InitializeObject(this);
			item.Copy(this);
			return item;
		}
		public override bool IsEqual(PersistentLib.PersistentObject copy) {
			return this.IsEqual(((ITokenReplayCache)(copy)));
		}
		public enum Fields {
			_Id,
			_TokenKey,
			_ExpirationTime,
			_SecurityToken,
		}
	}
}
