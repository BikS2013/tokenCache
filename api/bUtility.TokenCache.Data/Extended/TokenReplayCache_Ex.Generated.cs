namespace bUtility.TokenCache.Data {
	using PersistentLib;
	using System.Data;
	using System.Collections.Generic;
	using System.Xml;
	using System.Xml.Serialization;
	using Newtonsoft.Json;
	
	[System.SerializableAttribute()]
	public partial class TokenReplayCache_Ex : TokenReplayCache {
		static string SelectAll = "Select TokenReplayCache.Id, TokenReplayCache.TokenKey, TokenReplayCache.Expiratio" +
			"nTime, TokenReplayCache.SecurityToken from TokenReplayCache ";
		static string SelectBy_TokenKey = "Select TokenReplayCache.Id, TokenReplayCache.TokenKey, TokenReplayCache.Expiratio" +
			"nTime, TokenReplayCache.SecurityToken from TokenReplayCache where TokenKey = @To" +
			"kenKey ";
		static string pk = "TokenKey = @TokenKey ";
		public override object RowID {
			get {
				return _TokenKey;
			}
		}
		public event PersistentLib.UpdateNotification On_Updating;
		public event PersistentLib.UpdateNotification On_Updated;
		public event PersistentLib.UpdateNotification On_Deleting;
		public event PersistentLib.UpdateNotification On_Deleted;
		public virtual void UpdateParams_(PersistentLib.SqlParamDictionary paramlist) {
		}
		public static List<TokenReplayCache_Ex> FindAll(PersistentLib.ISqlFactory sf) {
			TokenReplayCache_Ex item = new TokenReplayCache_Ex();
			item.SqlFactory = sf;
			List<TokenReplayCache_Ex> data = item.SelectList<TokenReplayCache_Ex>(SelectAll, item.BuildParams_, ReadReader_ALL);
			return data;
		}
		public static List<T> FindAll<T>(PersistentLib.ISqlFactory sf)
			where T : TokenReplayCache_Ex, new () {
			T item = new T();
			item.SqlFactory = sf;
			List<T> data = item.SelectList<T>(SelectAll, item.BuildParams_, ReadReader_ALL);
			return data;
		}
		static void ParseRow_ALL(PersistentLib.PersistentObject item, System.Data.DataRow row) {
			((TokenReplayCache_Ex)(item))._Id = ((System.Nullable<System.Guid>)(GetRowValue(row, 0)));
			((TokenReplayCache_Ex)(item))._TokenKey = ((string)(GetRowValue(row, 1)));
			((TokenReplayCache_Ex)(item))._ExpirationTime = ((System.Nullable<System.DateTime>)(GetRowValue(row, 2)));
			((TokenReplayCache_Ex)(item))._SecurityToken = ((byte[])(GetRowValue(row, 3)));
		}
		static void ReadReader_ALL(PersistentLib.PersistentObject item, PersistentLib.ReaderWrapper reader) {
			((TokenReplayCache_Ex)(item))._Id = ((System.Nullable<System.Guid>)(reader.GetField(0)));
			((TokenReplayCache_Ex)(item))._TokenKey = ((string)(reader.GetField(1)));
			((TokenReplayCache_Ex)(item))._ExpirationTime = ((System.Nullable<System.DateTime>)(reader.GetField(2)));
			((TokenReplayCache_Ex)(item))._SecurityToken = ((byte[])(reader.GetField(3)));
		}
		public virtual void UpdateParams__TokenKey(PersistentLib.SqlParamDictionary paramlist) {
			SetParameterValue(paramlist, "@TokenKey", this._TokenKey);
		}
		public static TokenReplayCache_Ex FindByKey(PersistentLib.ISqlFactory sf, string tokenkey) {
			TokenReplayCache_Ex item = new TokenReplayCache_Ex();
			item.SqlFactory = sf;
			item._TokenKey = tokenkey;
			TokenReplayCache_Ex data = item.SelectSingleRow<TokenReplayCache_Ex>(SelectBy_TokenKey, item.BuildParams__TokenKey, ReadReader_ALL);
			return data;
		}
		public static T FindByKey<T>(PersistentLib.ISqlFactory sf, string tokenkey)
			where T : TokenReplayCache_Ex, new () {
			T item = new T();
			item.SqlFactory = sf;
			item._TokenKey = tokenkey;
			T data = item.SelectSingleRow<T>(SelectBy_TokenKey, item.BuildParams__TokenKey, ReadReader_ALL);
			return data;
		}
		public virtual System.Collections.Generic.List<System.Data.IDbDataParameter> BuildParams_() {
			System.Collections.Generic.List<System.Data.IDbDataParameter> paramlist = new System.Collections.Generic.List<System.Data.IDbDataParameter>();
			return paramlist;
		}
		public virtual System.Collections.Generic.List<System.Data.IDbDataParameter> BuildParams__TokenKey() {
			System.Collections.Generic.List<System.Data.IDbDataParameter> paramlist = new System.Collections.Generic.List<System.Data.IDbDataParameter>();
			System.Data.IDbDataParameter p_TokenKey = SqlFactory.CreateParam("@TokenKey", System.Data.SqlDbType.VarChar);
			SetParameterValue(p_TokenKey, _TokenKey);
			paramlist.Add(p_TokenKey);
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
