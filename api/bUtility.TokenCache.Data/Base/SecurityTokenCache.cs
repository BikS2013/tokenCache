namespace bUtility.TokenCache.Data {
	using System;
	using System.Text;
	using System.Diagnostics;
	using System.Data;
	using System.Xml.Serialization;
	using Newtonsoft.Json;
	using PersistentLib;
	
	[Serializable()]
	[Table("SecurityTokenCache")]
	public partial class SecurityTokenCache : PersistentLib.PersistentTable, ISecurityTokenCache {
		protected System.Nullable<System.Guid> _p_id;
		protected string _p_contextid;
		protected string _p_endpointid;
		protected string _p_keygeneration;
		protected System.Nullable<System.DateTime> _p_expirytime;
		protected System.Nullable<System.DateTime> _p_rollingexpirytime;
		protected byte[] _p_sessionsecuritytokenvalue;
		protected string _p_sessionsecuritytokenid;
		protected string _p_username;
		protected static string DefaultInsertQuery = @"Insert into SecurityTokenCache ( Id, ContextID, EndpointID, KeyGeneration, ExpiryTime, RollingExpiryTime, SessionSecurityTokenValue, SessionSecurityTokenID, UserName) Values ( @Id, @ContextID, @EndpointID, @KeyGeneration, @ExpiryTime, @RollingExpiryTime, @SessionSecurityTokenValue, @SessionSecurityTokenID, @UserName )";
		protected static string DefaultUpdateSetQuery = @"Id = @Id, ContextID = @ContextID, EndpointID = @EndpointID, KeyGeneration = @KeyGeneration, ExpiryTime = @ExpiryTime, RollingExpiryTime = @RollingExpiryTime, SessionSecurityTokenValue = @SessionSecurityTokenValue, SessionSecurityTokenID = @SessionSecurityTokenID, UserName = @UserName";
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
		[Column("ContextID", typeof(string), 100)]
		[Parameter("@ContextID", System.Data.SqlDbType.VarChar, 100, "ContextID")]
		public virtual string _ContextID {
			get {
				return this._p_contextid;
			}
			set {
				this._p_contextid = value;
			}
		}
		[Column("EndpointID", typeof(string), 100)]
		[Parameter("@EndpointID", System.Data.SqlDbType.VarChar, 100, "EndpointID")]
		public virtual string _EndpointID {
			get {
				return this._p_endpointid;
			}
			set {
				this._p_endpointid = value;
			}
		}
		[Column("KeyGeneration", typeof(string), 100)]
		[Parameter("@KeyGeneration", System.Data.SqlDbType.VarChar, 100, "KeyGeneration")]
		public virtual string _KeyGeneration {
			get {
				return this._p_keygeneration;
			}
			set {
				this._p_keygeneration = value;
			}
		}
		[Column("ExpiryTime", typeof(System.Nullable<System.DateTime>), false)]
		[Parameter("@ExpiryTime", System.Data.SqlDbType.DateTime, "ExpiryTime")]
		public virtual System.Nullable<System.DateTime> _ExpiryTime {
			get {
				return this._p_expirytime;
			}
			set {
				this._p_expirytime = value;
			}
		}
		[Column("RollingExpiryTime", typeof(System.Nullable<System.DateTime>), false)]
		[Parameter("@RollingExpiryTime", System.Data.SqlDbType.DateTime, "RollingExpiryTime")]
		public virtual System.Nullable<System.DateTime> _RollingExpiryTime {
			get {
				return this._p_rollingexpirytime;
			}
			set {
				this._p_rollingexpirytime = value;
			}
		}
		[Column("SessionSecurityTokenValue", typeof(byte[]), false)]
		[Parameter("@SessionSecurityTokenValue", System.Data.SqlDbType.VarBinary, "SessionSecurityTokenValue")]
		public virtual byte[] _SessionSecurityTokenValue {
			get {
				return this._p_sessionsecuritytokenvalue;
			}
			set {
				this._p_sessionsecuritytokenvalue = value;
			}
		}
		[Column("SessionSecurityTokenID", typeof(string), 100)]
		[Parameter("@SessionSecurityTokenID", System.Data.SqlDbType.VarChar, 100, "SessionSecurityTokenID")]
		public virtual string _SessionSecurityTokenID {
			get {
				return this._p_sessionsecuritytokenid;
			}
			set {
				this._p_sessionsecuritytokenid = value;
			}
		}
		[Column("UserName", typeof(string), 100)]
		[Parameter("@UserName", System.Data.SqlDbType.VarChar, 100, "UserName")]
		public virtual string _UserName {
			get {
				return this._p_username;
			}
			set {
				this._p_username = value;
			}
		}
		public override string PhysicalTableName {
			get {
				return "SecurityTokenCache";
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
		protected virtual void InternalCopy(SecurityTokenCache existingobject) {
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
		public override void ParseRow(System.Data.DataRow row) {
			_Id = ((System.Nullable<System.Guid>)(GetRowValue(row, "Id")));
			_ContextID = ((string)(GetRowValue(row, "ContextID")));
			_EndpointID = ((string)(GetRowValue(row, "EndpointID")));
			_KeyGeneration = ((string)(GetRowValue(row, "KeyGeneration")));
			_ExpiryTime = ((System.Nullable<System.DateTime>)(GetRowValue(row, "ExpiryTime")));
			_RollingExpiryTime = ((System.Nullable<System.DateTime>)(GetRowValue(row, "RollingExpiryTime")));
			_SessionSecurityTokenValue = ((byte[])(GetRowValue(row, "SessionSecurityTokenValue")));
			_SessionSecurityTokenID = ((string)(GetRowValue(row, "SessionSecurityTokenID")));
			_UserName = ((string)(GetRowValue(row, "UserName")));
		}
		public override void ReadReader(PersistentLib.PersistentObject item, PersistentLib.ReaderWrapper reader) {
			((SecurityTokenCache)(item))._Id = ((System.Nullable<System.Guid>)(reader.GetField("ID")));
			((SecurityTokenCache)(item))._ContextID = ((string)(reader.GetField("CONTEXTID")));
			((SecurityTokenCache)(item))._EndpointID = ((string)(reader.GetField("ENDPOINTID")));
			((SecurityTokenCache)(item))._KeyGeneration = ((string)(reader.GetField("KEYGENERATION")));
			((SecurityTokenCache)(item))._ExpiryTime = ((System.Nullable<System.DateTime>)(reader.GetField("EXPIRYTIME")));
			((SecurityTokenCache)(item))._RollingExpiryTime = ((System.Nullable<System.DateTime>)(reader.GetField("ROLLINGEXPIRYTIME")));
			((SecurityTokenCache)(item))._SessionSecurityTokenValue = ((byte[])(reader.GetField("SESSIONSECURITYTOKENVALUE")));
			((SecurityTokenCache)(item))._SessionSecurityTokenID = ((string)(reader.GetField("SESSIONSECURITYTOKENID")));
			((SecurityTokenCache)(item))._UserName = ((string)(reader.GetField("USERNAME")));
		}
		public override System.Data.DataRow ToRow(System.Data.DataRow row) {
			this.BeforeToRow(row);
			SetRowValue(row, "Id", this._Id);
			SetRowValue(row, "ContextID", this._ContextID);
			SetRowValue(row, "EndpointID", this._EndpointID);
			SetRowValue(row, "KeyGeneration", this._KeyGeneration);
			SetRowValue(row, "ExpiryTime", this._ExpiryTime);
			SetRowValue(row, "RollingExpiryTime", this._RollingExpiryTime);
			SetRowValue(row, "SessionSecurityTokenValue", this._SessionSecurityTokenValue);
			SetRowValue(row, "SessionSecurityTokenID", this._SessionSecurityTokenID);
			SetRowValue(row, "UserName", this._UserName);
			this.AfterToRow(row);
			return row;
		}
		public override void BuildDataParams() {
			this.DataParams = new PersistentLib.SqlParamDictionary();
			System.Data.IDbDataParameter p_Id = this.SqlFactory.CreateParam("@Id", System.Data.SqlDbType.UniqueIdentifier);
			p_Id.SourceColumn = "Id";
			this.DataParams.Add("@Id", p_Id);
			System.Data.IDbDataParameter p_ContextID = this.SqlFactory.CreateParam("@ContextID", System.Data.SqlDbType.VarChar);
			p_ContextID.SourceColumn = "ContextID";
			this.DataParams.Add("@ContextID", p_ContextID);
			System.Data.IDbDataParameter p_EndpointID = this.SqlFactory.CreateParam("@EndpointID", System.Data.SqlDbType.VarChar);
			p_EndpointID.SourceColumn = "EndpointID";
			this.DataParams.Add("@EndpointID", p_EndpointID);
			System.Data.IDbDataParameter p_KeyGeneration = this.SqlFactory.CreateParam("@KeyGeneration", System.Data.SqlDbType.VarChar);
			p_KeyGeneration.SourceColumn = "KeyGeneration";
			this.DataParams.Add("@KeyGeneration", p_KeyGeneration);
			System.Data.IDbDataParameter p_ExpiryTime = this.SqlFactory.CreateParam("@ExpiryTime", System.Data.SqlDbType.DateTime);
			p_ExpiryTime.SourceColumn = "ExpiryTime";
			this.DataParams.Add("@ExpiryTime", p_ExpiryTime);
			System.Data.IDbDataParameter p_RollingExpiryTime = this.SqlFactory.CreateParam("@RollingExpiryTime", System.Data.SqlDbType.DateTime);
			p_RollingExpiryTime.SourceColumn = "RollingExpiryTime";
			this.DataParams.Add("@RollingExpiryTime", p_RollingExpiryTime);
			System.Data.IDbDataParameter p_SessionSecurityTokenValue = this.SqlFactory.CreateParam("@SessionSecurityTokenValue", System.Data.SqlDbType.VarBinary);
			p_SessionSecurityTokenValue.SourceColumn = "SessionSecurityTokenValue";
			this.DataParams.Add("@SessionSecurityTokenValue", p_SessionSecurityTokenValue);
			System.Data.IDbDataParameter p_SessionSecurityTokenID = this.SqlFactory.CreateParam("@SessionSecurityTokenID", System.Data.SqlDbType.VarChar);
			p_SessionSecurityTokenID.SourceColumn = "SessionSecurityTokenID";
			this.DataParams.Add("@SessionSecurityTokenID", p_SessionSecurityTokenID);
			System.Data.IDbDataParameter p_UserName = this.SqlFactory.CreateParam("@UserName", System.Data.SqlDbType.VarChar);
			p_UserName.SourceColumn = "UserName";
			this.DataParams.Add("@UserName", p_UserName);
		}
		public override void UpdateDataParameters(PersistentLib.SqlParamDictionary paramlist) {
			SetParameterValue(paramlist, "@Id", this._Id);
			SetParameterValue(paramlist, "@ContextID", this._ContextID);
			SetParameterValue(paramlist, "@EndpointID", this._EndpointID);
			SetParameterValue(paramlist, "@KeyGeneration", this._KeyGeneration);
			SetParameterValue(paramlist, "@ExpiryTime", this._ExpiryTime);
			SetParameterValue(paramlist, "@RollingExpiryTime", this._RollingExpiryTime);
			SetParameterValue(paramlist, "@SessionSecurityTokenValue", this._SessionSecurityTokenValue);
			SetParameterValue(paramlist, "@SessionSecurityTokenID", this._SessionSecurityTokenID);
			SetParameterValue(paramlist, "@UserName", this._UserName);
		}
		public virtual void Copy(SecurityTokenCache existingobject) {
			this.InitializeObject(existingobject);
			this.Copy(((ISecurityTokenCache)(existingobject)));
		}
		public virtual SecurityTokenCache Copy() {
			SecurityTokenCache newobject = new SecurityTokenCache();
			newobject.Copy(this);
			return newobject;
		}
		public virtual T Copy<T>()
			where T : SecurityTokenCache, new () {
			T v = new T();
			v.Copy(this);
			return v;
		}
		private System.Xml.Serialization.XmlSerializer GetXmlSerializer() {
			return new System.Xml.Serialization.XmlSerializer(typeof(SecurityTokenCache));
		}
		public override void CopyDataFrom(PersistentLib.PersistentObject existing) {
			this.Copy(((ISecurityTokenCache)(existing)));
		}
		public override PersistentLib.PersistentObject GetCopy() {
			SecurityTokenCache item = new SecurityTokenCache();
			item.InitializeObject(this);
			item.Copy(this);
			return item;
		}
		public override bool IsEqual(PersistentLib.PersistentObject copy) {
			return this.IsEqual(((ISecurityTokenCache)(copy)));
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
