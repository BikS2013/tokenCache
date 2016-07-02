namespace bUtility.TokenCache.Data {
	using PersistentLib;
	using System.Data;
	using System.Collections.Generic;
	using System.Xml;
	using System.Xml.Serialization;
	using Newtonsoft.Json;
	
	[System.SerializableAttribute()]
	public partial class SecurityTokenCache_Ex : SecurityTokenCache {
		static string SelectAll = @"Select SecurityTokenCache.Id, SecurityTokenCache.ContextID, SecurityTokenCache.EndpointID, SecurityTokenCache.KeyGeneration, SecurityTokenCache.ExpiryTime, SecurityTokenCache.RollingExpiryTime, SecurityTokenCache.SessionSecurityTokenValue, SecurityTokenCache.SessionSecurityTokenID, SecurityTokenCache.UserName from SecurityTokenCache ";
		static string SelectBy_EndpointID_ContextID = @"Select SecurityTokenCache.Id, SecurityTokenCache.ContextID, SecurityTokenCache.EndpointID, SecurityTokenCache.KeyGeneration, SecurityTokenCache.ExpiryTime, SecurityTokenCache.RollingExpiryTime, SecurityTokenCache.SessionSecurityTokenValue, SecurityTokenCache.SessionSecurityTokenID, SecurityTokenCache.UserName from SecurityTokenCache where EndpointID = @EndpointID  and ContextID = @ContextID ";
		static string SelectBy_EndpointID_ContextID_KeyGeneration = @"Select SecurityTokenCache.Id, SecurityTokenCache.ContextID, SecurityTokenCache.EndpointID, SecurityTokenCache.KeyGeneration, SecurityTokenCache.ExpiryTime, SecurityTokenCache.RollingExpiryTime, SecurityTokenCache.SessionSecurityTokenValue, SecurityTokenCache.SessionSecurityTokenID, SecurityTokenCache.UserName from SecurityTokenCache where EndpointID = @EndpointID  and ContextID = @ContextID  and KeyGeneration = @KeyGeneration ";
		static string SelectBy_EndpointID_ContextID2 = @"Select SecurityTokenCache.Id, SecurityTokenCache.ContextID, SecurityTokenCache.EndpointID, SecurityTokenCache.KeyGeneration, SecurityTokenCache.ExpiryTime, SecurityTokenCache.RollingExpiryTime, SecurityTokenCache.SessionSecurityTokenValue, SecurityTokenCache.SessionSecurityTokenID, SecurityTokenCache.UserName from SecurityTokenCache where EndpointID = @EndpointID  and ContextID = @ContextID ";
		protected System.Nullable<System.DateTime> _p_currenttime;
		static string pk = "ContextID = @ContextID  and EndpointID = @EndpointID ";
		public virtual System.Nullable<System.DateTime> _CurrentTime {
			get {
				return this._p_currenttime;
			}
			set {
				this._p_currenttime = value;
			}
		}
		public event PersistentLib.UpdateNotification On_Updating;
		public event PersistentLib.UpdateNotification On_Updated;
		public event PersistentLib.UpdateNotification On_Deleting;
		public event PersistentLib.UpdateNotification On_Deleted;
		public virtual void UpdateParams_(PersistentLib.SqlParamDictionary paramlist) {
		}
		public static List<SecurityTokenCache_Ex> FindAll(PersistentLib.ISqlFactory sf) {
			SecurityTokenCache_Ex item = new SecurityTokenCache_Ex();
			item.SqlFactory = sf;
			List<SecurityTokenCache_Ex> data = item.SelectList<SecurityTokenCache_Ex>(SelectAll, item.BuildParams_, ReadReader_ALL);
			return data;
		}
		public static List<T> FindAll<T>(PersistentLib.ISqlFactory sf)
			where T : SecurityTokenCache_Ex, new () {
			T item = new T();
			item.SqlFactory = sf;
			List<T> data = item.SelectList<T>(SelectAll, item.BuildParams_, ReadReader_ALL);
			return data;
		}
		static void ParseRow_ALL(PersistentLib.PersistentObject item, System.Data.DataRow row) {
			((SecurityTokenCache_Ex)(item))._Id = ((System.Nullable<System.Guid>)(GetRowValue(row, 0)));
			((SecurityTokenCache_Ex)(item))._ContextID = ((string)(GetRowValue(row, 1)));
			((SecurityTokenCache_Ex)(item))._EndpointID = ((string)(GetRowValue(row, 2)));
			((SecurityTokenCache_Ex)(item))._KeyGeneration = ((string)(GetRowValue(row, 3)));
			((SecurityTokenCache_Ex)(item))._ExpiryTime = ((System.Nullable<System.DateTime>)(GetRowValue(row, 4)));
			((SecurityTokenCache_Ex)(item))._RollingExpiryTime = ((System.Nullable<System.DateTime>)(GetRowValue(row, 5)));
			((SecurityTokenCache_Ex)(item))._SessionSecurityTokenValue = ((byte[])(GetRowValue(row, 6)));
			((SecurityTokenCache_Ex)(item))._SessionSecurityTokenID = ((string)(GetRowValue(row, 7)));
			((SecurityTokenCache_Ex)(item))._UserName = ((string)(GetRowValue(row, 8)));
		}
		static void ReadReader_ALL(PersistentLib.PersistentObject item, PersistentLib.ReaderWrapper reader) {
			((SecurityTokenCache_Ex)(item))._Id = ((System.Nullable<System.Guid>)(reader.GetField(0)));
			((SecurityTokenCache_Ex)(item))._ContextID = ((string)(reader.GetField(1)));
			((SecurityTokenCache_Ex)(item))._EndpointID = ((string)(reader.GetField(2)));
			((SecurityTokenCache_Ex)(item))._KeyGeneration = ((string)(reader.GetField(3)));
			((SecurityTokenCache_Ex)(item))._ExpiryTime = ((System.Nullable<System.DateTime>)(reader.GetField(4)));
			((SecurityTokenCache_Ex)(item))._RollingExpiryTime = ((System.Nullable<System.DateTime>)(reader.GetField(5)));
			((SecurityTokenCache_Ex)(item))._SessionSecurityTokenValue = ((byte[])(reader.GetField(6)));
			((SecurityTokenCache_Ex)(item))._SessionSecurityTokenID = ((string)(reader.GetField(7)));
			((SecurityTokenCache_Ex)(item))._UserName = ((string)(reader.GetField(8)));
		}
		public virtual void UpdateParams__EndpointID_ContextID(PersistentLib.SqlParamDictionary paramlist) {
			SetParameterValue(paramlist, "@EndpointID", this._EndpointID);
			SetParameterValue(paramlist, "@ContextID", this._ContextID);
		}
		public static List<SecurityTokenCache_Ex> FindAllByEndpointIDAndContextID(PersistentLib.ISqlFactory sf, string endpointid, string contextid) {
			SecurityTokenCache_Ex item = new SecurityTokenCache_Ex();
			item.SqlFactory = sf;
			item._EndpointID = endpointid;
			item._ContextID = contextid;
			List<SecurityTokenCache_Ex> data = item.SelectList<SecurityTokenCache_Ex>(SelectBy_EndpointID_ContextID, item.BuildParams__EndpointID_ContextID, ReadReader_ALL);
			return data;
		}
		public static List<T> FindAllByEndpointIDAndContextID<T>(PersistentLib.ISqlFactory sf, string endpointid, string contextid)
			where T : SecurityTokenCache_Ex, new () {
			T item = new T();
			item.SqlFactory = sf;
			item._EndpointID = endpointid;
			item._ContextID = contextid;
			List<T> data = item.SelectList<T>(SelectBy_EndpointID_ContextID, item.BuildParams__EndpointID_ContextID, ReadReader_ALL);
			return data;
		}
		public virtual void UpdateParams__EndpointID_ContextID_KeyGeneration(PersistentLib.SqlParamDictionary paramlist) {
			SetParameterValue(paramlist, "@EndpointID", this._EndpointID);
			SetParameterValue(paramlist, "@ContextID", this._ContextID);
			SetParameterValue(paramlist, "@KeyGeneration", this._KeyGeneration);
		}
		public static SecurityTokenCache_Ex FindByEndpointIDContextIDAndKeyGeneration(PersistentLib.ISqlFactory sf, string endpointid, string contextid, string keygeneration) {
			SecurityTokenCache_Ex item = new SecurityTokenCache_Ex();
			item.SqlFactory = sf;
			item._EndpointID = endpointid;
			item._ContextID = contextid;
			item._KeyGeneration = keygeneration;
			SecurityTokenCache_Ex data = item.SelectSingleRow<SecurityTokenCache_Ex>(SelectBy_EndpointID_ContextID_KeyGeneration, item.BuildParams__EndpointID_ContextID_KeyGeneration, ReadReader_ALL);
			return data;
		}
		public static T FindByEndpointIDContextIDAndKeyGeneration<T>(PersistentLib.ISqlFactory sf, string endpointid, string contextid, string keygeneration)
			where T : SecurityTokenCache_Ex, new () {
			T item = new T();
			item.SqlFactory = sf;
			item._EndpointID = endpointid;
			item._ContextID = contextid;
			item._KeyGeneration = keygeneration;
			T data = item.SelectSingleRow<T>(SelectBy_EndpointID_ContextID_KeyGeneration, item.BuildParams__EndpointID_ContextID_KeyGeneration, ReadReader_ALL);
			return data;
		}
		public static SecurityTokenCache_Ex FindByEndpointIDAndContextID(PersistentLib.ISqlFactory sf, string endpointid, string contextid) {
			SecurityTokenCache_Ex item = new SecurityTokenCache_Ex();
			item.SqlFactory = sf;
			item._EndpointID = endpointid;
			item._ContextID = contextid;
			SecurityTokenCache_Ex data = item.SelectSingleRow<SecurityTokenCache_Ex>(SelectBy_EndpointID_ContextID2, item.BuildParams__EndpointID_ContextID, ReadReader_ALL);
			return data;
		}
		public static T FindByEndpointIDAndContextID<T>(PersistentLib.ISqlFactory sf, string endpointid, string contextid)
			where T : SecurityTokenCache_Ex, new () {
			T item = new T();
			item.SqlFactory = sf;
			item._EndpointID = endpointid;
			item._ContextID = contextid;
			T data = item.SelectSingleRow<T>(SelectBy_EndpointID_ContextID2, item.BuildParams__EndpointID_ContextID, ReadReader_ALL);
			return data;
		}
		public override void BuildDataParams() {
			base.BuildDataParams();
			System.Data.IDbDataParameter p_CurrentTime = this.SqlFactory.CreateParam("@CurrentTime", System.Data.SqlDbType.DateTime);
			p_CurrentTime.SourceColumn = "CurrentTime";
			this.DataParams.Add("@CurrentTime", p_CurrentTime);
		}
		public override void UpdateDataParameters(PersistentLib.SqlParamDictionary paramlist) {
			base.UpdateDataParameters(paramlist);
			SetParameterValue(paramlist, "@CurrentTime", this._CurrentTime);
		}
		public override void ParseRow(System.Data.DataRow row) {
			base.ParseRow(row);
			_CurrentTime = ((System.Nullable<System.DateTime>)(GetRowValue(row, "CurrentTime")));
		}
		public override void ReadReader(PersistentLib.PersistentObject item, PersistentLib.ReaderWrapper reader) {
			base.ReadReader(item, reader);
			((SecurityTokenCache_Ex)(item))._CurrentTime = ((System.Nullable<System.DateTime>)(reader.GetField("CURRENTTIME")));
		}
		public virtual System.Collections.Generic.List<System.Data.IDbDataParameter> BuildParams_() {
			System.Collections.Generic.List<System.Data.IDbDataParameter> paramlist = new System.Collections.Generic.List<System.Data.IDbDataParameter>();
			return paramlist;
		}
		public virtual System.Collections.Generic.List<System.Data.IDbDataParameter> BuildParams__EndpointID_ContextID() {
			System.Collections.Generic.List<System.Data.IDbDataParameter> paramlist = new System.Collections.Generic.List<System.Data.IDbDataParameter>();
			System.Data.IDbDataParameter p_EndpointID = SqlFactory.CreateParam("@EndpointID", System.Data.SqlDbType.VarChar);
			SetParameterValue(p_EndpointID, _EndpointID);
			paramlist.Add(p_EndpointID);
			System.Data.IDbDataParameter p_ContextID = SqlFactory.CreateParam("@ContextID", System.Data.SqlDbType.VarChar);
			SetParameterValue(p_ContextID, _ContextID);
			paramlist.Add(p_ContextID);
			return paramlist;
		}
		public virtual System.Collections.Generic.List<System.Data.IDbDataParameter> BuildParams__EndpointID_ContextID_KeyGeneration() {
			System.Collections.Generic.List<System.Data.IDbDataParameter> paramlist = new System.Collections.Generic.List<System.Data.IDbDataParameter>();
			System.Data.IDbDataParameter p_EndpointID = SqlFactory.CreateParam("@EndpointID", System.Data.SqlDbType.VarChar);
			SetParameterValue(p_EndpointID, _EndpointID);
			paramlist.Add(p_EndpointID);
			System.Data.IDbDataParameter p_ContextID = SqlFactory.CreateParam("@ContextID", System.Data.SqlDbType.VarChar);
			SetParameterValue(p_ContextID, _ContextID);
			paramlist.Add(p_ContextID);
			System.Data.IDbDataParameter p_KeyGeneration = SqlFactory.CreateParam("@KeyGeneration", System.Data.SqlDbType.VarChar);
			SetParameterValue(p_KeyGeneration, _KeyGeneration);
			paramlist.Add(p_KeyGeneration);
			return paramlist;
		}
		public override int Update() {
			if ((this.On_Updating != null)) {
				this.On_Updating();
			}
			int res;
			res = this.Update(null, pk);
			if ((this.On_Updated != null)) {
				this.On_Updated();
			}
			return res;
		}
		public override int Delete() {
			if ((this.On_Deleting != null)) {
				this.On_Deleting();
			}
			int res;
			res = this.Delete(pk);
			if ((this.On_Deleted != null)) {
				this.On_Deleted();
			}
			return res;
		}
	}
}
