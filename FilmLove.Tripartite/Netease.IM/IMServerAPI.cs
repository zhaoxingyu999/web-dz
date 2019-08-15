using FilmLove.Common;
using FilmLove.Tripartite.Netease.IM.Model.IMResEntity;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace Dualpsy.Tripartite.Netease.IM
{

    public class IMServerAPI
    {
        #region 创建IM账户 UserCreate
        /// <summary>
        /// 创建IM账户
        /// </summary>
        /// <param name="Accid">用户ID</param>
        /// <returns>Token</returns>
        public IMUserCreateRes UserCreate(string accid, string nickname)
        {
            string url = "https://api.netease.im/nimserver/user/create.action";
            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("accid", accid);
            if (!string.IsNullOrWhiteSpace(nickname))
                data.Add("name", nickname);
            string str = IMHttpPost(url, data);
            IMUserCreateRes res = JsonConvert.DeserializeObject<IMUserCreateRes>(str);
            return res;
        }
        #endregion
        /// <summary>
        /// 更新头像与昵称
        /// </summary>
        /// <param name="accid"></param>
        /// <param name="icon"></param>
        /// <param name="nickname"></param>
        /// <returns></returns>
        public IMUserCreateRes UpdateUserinfo(string accid, string icon, string nickname)
        {
            string url = "https://api.netease.im/nimserver/user/updateUinfo.action";
            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("accid", accid);
            if (icon != null)
                data.Add("icon", icon);
            if (nickname != null)
                data.Add("name", nickname);
            string str = IMHttpPost(url, data);
            IMUserCreateRes res = JsonConvert.DeserializeObject<IMUserCreateRes>(str);
            return res;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="accid">加好友发起者accid</param>
        /// <param name="faccid">加好友接收者accid</param>
        /// <param name="type">2请求加好友，3同意加好友，4拒绝加好友</param>
        /// <returns></returns>
        public IMUserCreateRes FriendAdd(string accid, string faccid, int type)
        {
            string url = "https://api.netease.im/nimserver/friend/add.action";
            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("accid", accid);
            data.Add("faccid", faccid);
            data.Add("type", type + "");
            string str = IMHttpPost(url, data);
            IMUserCreateRes res = JsonConvert.DeserializeObject<IMUserCreateRes>(str);
            return res;
        }
        /// <summary>
        /// 删除好友
        /// </summary>
        /// <param name="accid"></param>
        /// <param name="faccid"></param>
        /// <returns></returns>
        public IMUserCreateRes FriendDelete(string accid, string faccid)
        {
            string url = "https://api.netease.im/nimserver/friend/delete.action";
            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("accid", accid);
            data.Add("faccid", faccid);
            string str = IMHttpPost(url, data);
            IMUserCreateRes res = JsonConvert.DeserializeObject<IMUserCreateRes>(str);
            return res;
        }
        /// <summary>
        /// 设置静音
        /// </summary>
        /// <param name="accid"></param>
        /// <param name="targetAcc"></param>
        /// <param name="type">0:取消静音，1:加入静音</param>
        /// <returns></returns>
        public IMUserCreateRes FriendMute(string accid, string targetAcc, int type)
        {
            string url = "https://api.netease.im/nimserver/user/setSpecialRelation.action";
            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("accid", accid);
            data.Add("targetAcc", targetAcc);
            data.Add("relationType", "2");
            data.Add("value", type + "");
            string str = IMHttpPost(url, data);
            IMUserCreateRes res = JsonConvert.DeserializeObject<IMUserCreateRes>(str);
            return res;
        }

        #region 刷新ACCID的Token UserRefreshToken
        /// <summary>
        /// 刷新ACCID的Token
        /// </summary>
        /// <param name="AccID"></param>
        /// <returns></returns>
        public IMUserRefreshTokenRes UserRefreshToken(string AccID)
        {
            String url = "https://api.netease.im/nimserver/user/refreshToken.action";
            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("accid", AccID);
            string str = IMHttpPost(url, data);
            IMUserRefreshTokenRes res = JsonConvert.DeserializeObject<IMUserRefreshTokenRes>(str);
            return res;
        }
        #endregion

        public IMUserCreateRes GetUinfos(List<string> accid)
        {
            string url = "https://api.netease.im/nimserver/user/getUinfos.action";
            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("accids", Newtonsoft.Json.JsonConvert.SerializeObject(accid));
            string str = IMHttpPost(url, data);
            IMUserCreateRes res = JsonConvert.DeserializeObject<IMUserCreateRes>(str);
            return res;
        }

        #region 创建房间 TeamCreate
        /// <summary>
        /// 创建房间
        /// </summary>
        /// <param name="GameID">游戏ＩＤ</param>
        /// <param name="RoomName">房间名称</param>
        /// <param name="AdminAccID">房间创建人AccID</param>
        /// <param name="Num">房间顺序号</param>
        /// <returns></returns>
        //public IMTeamCreateRes TeamCreate(int GameID, string RoomName, string AdminAccID, int Num)
        //{
        //    string url = "https://api.netease.im/nimserver/team/create.action";
        //    var m = new
        //    {
        //        GameID = GameID.ToString(),
        //    };
        //    string customjson = JsonConvert.SerializeObject(m);
        //    string[] strs = new string[] { AdminAccID };
        //    string accids = JsonConvert.SerializeObject(strs);
        //    Dictionary<string, string> data = new Dictionary<string, string>();
        //    data.Add("tname", RoomName);
        //    data.Add("owner", AdminAccID);
        //    data.Add("members", accids);
        //    data.Add("magree", "0");
        //    data.Add("joinmode", "0");
        //    data.Add("msg", "welcome");
        //    data.Add("custom", customjson);
        //    string str = IMHttpPost(url, data);
        //    IMTeamCreateRes res = JsonConvert.DeserializeObject<IMTeamCreateRes>(str);
        //    if (res.code == 200)
        //    {
        //        HalfEntities db = new HalfEntities();
        //        BaseGameRoom ent = new BaseGameRoom();
        //        ent.RoomId = res.tid;
        //        ent.RoomName = RoomName;
        //        ent.CreateTime = DateTime.Now;
        //        ent.GameId = GameID;
        //        ent.RoomNum = Num;
        //        db.BaseGameRoom.Add(ent);
        //        db.SaveChanges();
        //    }
        //    return res;
        //}
        #endregion

        #region 获取群成员 TeamQueryDetail
        /// <summary>
        /// 获取群成员
        /// </summary>
        /// <param name="tid">群ＩＤ</param>
        //public IMTeamQueryDetailRes TeamQueryDetail(string tid)
        //{
        //    string url = "https://api.netease.im/nimserver/team/queryDetail.action";
        //    Dictionary<string, string> data = new Dictionary<string, string>();
        //    data.Add("tid", tid);
        //    string str = IMHttpPost(url, data);
        //    IMTeamQueryDetailRes res = JsonConvert.DeserializeObject<IMTeamQueryDetailRes>(str);
        //    return res;
        //}
        #endregion

        #region 删除房间 TeamRemove
        /// <summary>
        /// 删除房间
        /// </summary>
        /// <param name="Tid">房间ＩＤ</param>
        /// <param name="AdminAccID">房间拥有人</param>
        /// <returns></returns>
        //public IMTeamRemoveRes TeamRemove(string tid, string owner)
        //{
        //    string url = "https://api.netease.im/nimserver/team/remove.action";
        //    Dictionary<string, string> data = new Dictionary<string, string>();
        //    data.Add("tid", tid);
        //    data.Add("owner", owner);
        //    string str = IMHttpPost(url, data);
        //    IMTeamRemoveRes res = JsonConvert.DeserializeObject<IMTeamRemoveRes>(str);
        //    return res;
        //}
        #endregion

        #region 加入的群 TeamJoinTeams
        /// <summary>
        /// 加入的群
        /// </summary>
        /// <param name="accid">网易云用户ID</param>
        /// <returns></returns>
        //public IMTeamJoinTeamsRes TeamJoinTeams(string accid)
        //{
        //    string url = "https://api.netease.im/nimserver/team/joinTeams.action";
        //    Dictionary<string, string> data = new Dictionary<string, string>();
        //    data.Add("accid", accid);
        //    string str = IMHttpPost(url, data);
        //    IMTeamJoinTeamsRes res = JsonConvert.DeserializeObject<IMTeamJoinTeamsRes>(str);
        //    return res;
        //}
        #endregion

        #region 拉人入群 Join_Team
        /// <summary>
        ///　拉人入群
        /// </summary>
        /// <param name="accid"></param>
        /// <param name="tid">群ID</param>
        /// <param name="owner"></param>
        /// <returns></returns>
        //public IMTeamAddRes TeamAdd(string tid, string owner, List<string> members)
        //{
        //    string url = "https://api.netease.im/nimserver/team/add.action";
        //    Dictionary<string, string> data = new Dictionary<string, string>();
        //    data.Add("tid", tid);
        //    data.Add("owner", owner);
        //    data.Add("members", JsonConvert.SerializeObject(members));
        //    data.Add("msg", "welcome");
        //    data.Add("magree", "0");
        //    string str = IMHttpPost(url, data);
        //    IMTeamAddRes res = JsonConvert.DeserializeObject<IMTeamAddRes>(str);
        //    return res;
        //}
        #endregion

        #region 群踢人 TeamKick
        /// <summary>
        /// 群踢人
        /// </summary>
        /// <param name="tid">IM 群ID</param>
        /// <param name="member"></param>
        /// <param name="members"></param>
        /// <param name="owner"></param>
        /// <returns></returns>
        //public IMTeamKickRes TeamKick(string tid, List<string> members, string owner)
        //{
        //    if (members.Count <= 0)
        //        return new IMTeamKickRes() { code = 200 };
        //    string url = "https://api.netease.im/nimserver/team/kick.action";
        //    Dictionary<string, string> data = new Dictionary<string, string>();
        //    data.Add("tid", tid);
        //    data.Add("owner", owner);
        //    if (members.Count == 1)
        //        data.Add("member", members[0]);
        //    else
        //        data.Add("members", JsonConvert.SerializeObject(members));
        //    string str = IMHttpPost(url, data);
        //    IMTeamKickRes res = JsonConvert.DeserializeObject<IMTeamKickRes>(str);
        //    return res;
        //}
        #endregion

        #region 发送普通消息 MsgSendMsg
        /// <summary>
        /// 发送普通消息
        /// </summary>
        /// <param name="msgStr"></param>
        /// <returns></returns>
        public IMMsgSendMsgRes MsgSendMsg(string to, string msgStr, string ext)
        {
            string url = "https://api.netease.im/nimserver/msg/sendMsg.action";
            Dictionary<string, string> data = new Dictionary<string, string>();
            var body = new
            {
                msg = msgStr,
            };
            data.Add("from", "-1");
            data.Add("ope", "0");// 0：点对点个人消息，1：群消息（高级群），其他返回414
            data.Add("to", to);//ope==0是表示accid即用户id，ope==1表示tid即群id
            //0 表示文本消息,
            //1 表示图片，
            //2 表示语音，
            //3 表示视频，
            //4 表示地理位置信息，
            //6 表示文件，
            //100 自定义消息类型（特别注意，对于未对接易盾反垃圾功能的应用，该类型的消息不会提交反垃圾系统检测）
            data.Add("type", "0");
            data.Add("body", JsonConvert.SerializeObject(body));
            data.Add("ext", ext);
            string str = IMHttpPost(url, data);
            IMMsgSendMsgRes res = JsonConvert.DeserializeObject<IMMsgSendMsgRes>(str);
            return res;
        }
        #endregion

        #region 发送自定义系统通知 MsgSendAttachMsg
        /// <summary>
        /// 发送自定义系统通知
        /// </summary>
        /// <param name="msgStr"></param>
        /// <returns></returns>
        public IMMsgSendAttachMsgRes MsgSendAttachMsg(string to, string msgStr)
        {
            string url = "https://api.netease.im/nimserver/msg/sendAttachMsg.action";
            Dictionary<string, string> data = new Dictionary<string, string>();
            var body = new
            {
                msgStr = msgStr,
            };
            data.Add("from", "-1");
            data.Add("msgtype", "0");   //0：点对点自定义通知，1：群消息自定义通知，其他返回414
            data.Add("to", to);
            data.Add("attach", JsonConvert.SerializeObject(body));
            string str = IMHttpPost(url, data);
            IMMsgSendAttachMsgRes res = JsonConvert.DeserializeObject<IMMsgSendAttachMsgRes>(str);
            return res;
        }
        #endregion

        #region 测试方法
        /// <summary>
        /// 移除群所有成员
        /// </summary>
        /// <param name="tid"></param>
        /// <param name="owner"></param>
        /// <returns></returns>
        //public void TeamClear(string tid, string owner)
        //{
        //    var model1 = TeamQueryDetail(tid);
        //    if (model1.code == 200)
        //    {
        //        List<string> list = new List<string>();
        //        if (model1.tinfo.members == null)
        //            return;
        //        if (model1.tinfo.members.Count == 0)
        //            return;
        //        foreach (var model2 in model1.tinfo.members)
        //        {
        //            list.Add(model2.accid);
        //        }
        //        TeamKick(tid, list, owner);
        //    }
        //}
        //public void MyTest()
        //{
        //    string owner = Configure.IMRobotAccount;
        //    var ent = GetJoinTeams(owner);

        //    Console.WriteLine(JsonConvert.SerializeObject(ent));
        //    if (ent.infos == null)
        //        return;
        //    foreach (var item in ent.infos)
        //    {
        //        var r = Remove_Room(item.tid, owner);
        //        Console.WriteLine(JsonConvert.SerializeObject(r));

        //        var detail = TeamQueryDetail(item.tid);
        //        if(detail.code == "200")
        //        {
        //            var accids = detail.tinfo.members.Select(m => m.accid).ToList();
        //            TeamKick(item.tid, accids, owner);
        //        }
        //        Console.WriteLine(JsonConvert.SerializeObject(detail));
        //    }
        //}

        //public void TestRemoveAllRoom()
        //{
        //    string owner = Configure.IMRobotAccount;
        //    var ent = TeamJoinTeams(owner);

        //    Console.WriteLine(JsonConvert.SerializeObject(ent));
        //    if (ent.infos == null)
        //        return;
        //    foreach (var item in ent.infos)
        //    {
        //        var r = TeamRemove(item.tid, owner);
        //        Console.WriteLine(JsonConvert.SerializeObject(r));
        //    }
        //}
        #endregion

        #region 辅助方法
        /// <summary>
        /// IMHttpPost
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        private string IMHttpPost(string url, Dictionary<string, string> postdata)
        {
            string Nonce = GetRdString();
            TimeSpan ts = DateTime.Now.ToUniversalTime() - new DateTime(1970, 1, 1);
            string CurTime = ((int)ts.TotalSeconds).ToString();
            string CheckSum = getCheckSum(Configure.IMAppSecret, Nonce, CurTime);
            IDictionary<object, string> headers = new Dictionary<object, string>();
            headers.Add("AppKey", Configure.IMAppKey);
            headers.Add("Nonce", Nonce);
            headers.Add("CurTime", CurTime);
            headers.Add("CheckSum", CheckSum);
            var data = GetPostData(postdata);
            return HttpPost(url, data, headers);
        }


        private byte[] GetPostData(Dictionary<string, string> postdata)
        {
            List<string> strList = new List<string>();
            foreach (var item in postdata)
            {
                strList.Add(string.Format("{0}={1}", item.Key, item.Value));
            }
            if (strList.Count == 0)
                return null;
            string qStr = string.Join("&", strList.ToArray());
            return Encoding.UTF8.GetBytes(qStr);
        }


        /// <summary>
        /// 获取随机串
        /// </summary>
        /// <returns></returns>
        private string GetRdString()
        {
            Random rd = new Random();
            return rd.Next(100000, 999999).ToString();
        }


        private string getFormattedText(byte[] bytes)
        {
            char[] HEX_DIGITS = { '0', '1', '2', '3', '4', '5',
            '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f' };
            int len = bytes.Length;
            StringBuilder buf = new StringBuilder(len * 2);
            for (int j = 0; j < len; j++)
            {
                buf.Append(HEX_DIGITS[(bytes[j] >> 4) & 0x0f]);
                buf.Append(HEX_DIGITS[bytes[j] & 0x0f]);
            }
            return buf.ToString();
        }

        // 计算并获取CheckSum
        private string getCheckSum(string appSecret, string nonce, string curTime)
        {
            byte[] data = Encoding.Default.GetBytes(appSecret + nonce + curTime);
            byte[] result;

            SHA1 sha = new SHA1CryptoServiceProvider();
            // This is one implementation of the abstract class SHA1.
            result = sha.ComputeHash(data);
            return getFormattedText(result);
        }

        private string HttpPost(string url, byte[] data, IDictionary<object, string> headers = null)
        {

            string str = "";
            System.Net.WebRequest request = HttpWebRequest.Create(url);
            request.Method = "POST";
            if (data != null)
                request.ContentLength = data.Length;
            request.ContentType = "application/x-www-form-urlencoded;charset=utf-8";
            if (headers != null)
            {
                foreach (var v in headers)
                {
                    if (v.Key is HttpRequestHeader)
                        request.Headers[(HttpRequestHeader)v.Key] = v.Value;
                    else
                        request.Headers[v.Key.ToString()] = v.Value;
                }
            }

            Stream writer = request.GetRequestStream();
            writer.Write(data, 0, data.Length);
            writer.Close();
            HttpWebResponse response = null;
            try
            {
                response = (HttpWebResponse)request.GetResponse();
                Stream dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                str = reader.ReadToEnd();
                reader.Close();
                dataStream.Close();
                response.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(response.StatusDescription);
            }
            finally
            {
                if (request != null)
                    request.Abort();
            }
            return str;
        }
        #endregion
    }
}