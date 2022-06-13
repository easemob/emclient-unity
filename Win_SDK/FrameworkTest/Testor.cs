using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using ChatSDK;
using ChatSDK.MessageBody;

namespace WinSDKTest
{
    class SelectContext
    {
        public string level1_item; // manager
        public string level2_item; // function
        public List<string> level3_params; // param values in string format
        public int current_level;   // 1 - level1; 2 - level2; 3 - level3
        public Dictionary<string, int> group_invitation_select; //<groupid, acceptornot> acceptornot: 0 - not gree to accept; 1 - gree to accept
        public Dictionary<string, int> group_application_select; //<groupid, acceptornot> acceptornot: 0 - not gree to accept; 1 - gree to accept
    }

    class Utils
    {
        public static string GetMaxLenString(Dictionary<int, string> dic)
        {
            string ret = "";
            if (dic.Count == 0) return ret;

            foreach(var it in dic)
            {
                if(ret.Length < it.Value.Length)
                {
                    ret = it.Value;
                }
            }
            return ret;
        }

        public static List<string> SplitString(string s)
        {
            List<string> list = new List<string>();
            if (s.Length == 0) return list;

            string[] split = s.Split(new Char[] { ';'});
            for(int i=0; i<split.Length; i++)
            {
                list.Add(split[i]);
            }

            return list;
        }
    }

    /*
     * leve11 menu=======
     * Please select test manager below:
     * 
     * 1. ChatManager
     * 2. ContatctManager
     * 3. GroupManager
     * 
     * Your select: 
     * 
     * leve12 menu=======
     * 
     * level2 menu:
     * Please select test function below:
     * Current Manager: ChatManager
     * 
     * 1. SendMessage                             2. GetUnreadMessageCount              3. FuncA
     * 4. FuncB                                   5. ImportMessages
     * 3. DownloadAttachment                      7. LoadAllConversations
     * 4. FetchHistoryMessagesFromServer          8. MarkAllConversationsAsRead
     * 
     * Your select: I
     * 
     * leve13 menu=======
     * 
     * Current Manager: ChatManager
     * Please fill parameters for function: FetchHistoryMessagesFromServer
     * 
     * 1. conversationId
     * 2. conversation_Type (0:Chat; 1:Group; 2:Room)
     * 3. startMessageId
     * 4. count
     * 
     * Your Input:
     * yqtest1;0;;250
     * 
     * Running case, result is:
     * 
     * xxxxx
     * 
     * Press any key to return main menu:
     * 
     */

    class Testor
    {
        internal Dictionary<int, string> level1_menus;  // managers
        internal Dictionary<string, Dictionary<int, string>> level2_menus; // functions
        internal Dictionary<string, Dictionary<int, string>> level3_menus; // parameter names
        SelectContext select_context;

        internal Int64 GetTS()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds);
        }

        internal string GetParamValueFromContext(int n)
        {
            string ret = "";
            if (null != select_context)
            {
                if (null != select_context.level3_params && select_context.level3_params.Count > n)
                {
                    ret = select_context.level3_params[n];
                }
            }
            return ret;
        }

        internal int GetIntFromString(string str)
        {
            int ret = -1;
            if (str.Length > 0)
            {
                int iResult = 0;
                if (int.TryParse(str, out iResult) == true)
                {
                    ret = int.Parse(str);
                }
            }
            return ret;
        }

        internal long GetLongFromString(string str)
        {
            long ret = -1;
            if (str.Length > 0)
            {
                long iResult = 0;
                if (long.TryParse(str, out iResult) == true)
                {
                    ret = long.Parse(str);
                }
            }
            return ret;
        }

        public Testor()
        {
            select_context = new SelectContext();
            select_context.group_invitation_select = new Dictionary<string, int>();
            select_context.group_application_select = new Dictionary<string, int>();
            level1_menus = new Dictionary<int, string>();
            level2_menus = new Dictionary<string, Dictionary<int, string>>();
            level3_menus = new Dictionary<string, Dictionary<int, string>>();
        }

        ~Testor()
        {
            if (null != level1_menus) level1_menus.Clear();
            if (null != level2_menus) level2_menus.Clear();
            if (null != level3_menus) level3_menus.Clear();
        }

        internal void InitLevel1Menus()
        {
            int menu_index = 1;
            level1_menus.Add(menu_index, "IClient"); menu_index++;
            level1_menus.Add(menu_index, "IChatManager"); menu_index++;
            level1_menus.Add(menu_index, "IContactManager"); menu_index++;
            level1_menus.Add(menu_index, "IConversationManager"); menu_index++;
            level1_menus.Add(menu_index, "IGroupManager"); menu_index++;
            level1_menus.Add(menu_index, "IPushManager"); menu_index++;
            level1_menus.Add(menu_index, "IRoomManager"); menu_index++;
            level1_menus.Add(menu_index, "IUserInfoManager");
        }

        internal void InitLevel2Menus_IClient()
        {
            Dictionary<int, string> functions_IClient = new Dictionary<int, string>();
            int menu_index = 1;
            functions_IClient.Add(menu_index, "CreateAccount"); menu_index++;
            functions_IClient.Add(menu_index, "Login"); menu_index++;
            functions_IClient.Add(menu_index, "Logout"); menu_index++;
            functions_IClient.Add(menu_index, "CurrentUsername"); menu_index++;
            functions_IClient.Add(menu_index, "IsConnected"); menu_index++;
            functions_IClient.Add(menu_index, "IsLoggedIn"); menu_index++;
            functions_IClient.Add(menu_index, "AccessToken"); menu_index++;
            functions_IClient.Add(menu_index, "LoginWithAgoraToken"); menu_index++;
            functions_IClient.Add(menu_index, "RenewAgoraToken"); menu_index++;
            level2_menus.Add("IClient", functions_IClient);
        }

        internal void InitLevel3Menus_IClient()
        {
            Dictionary<int, string> param = new Dictionary<int, string>();
            int menu_index = 1;
            param.Add(menu_index, "username (string)"); menu_index++;
            param.Add(menu_index, "password (string)"); menu_index++;
            level3_menus.Add("CreateAccount", new Dictionary<int, string>(param));
            param.Clear();

            menu_index = 1;
            param.Add(menu_index, "username (string)"); menu_index++;
            param.Add(menu_index, "password (string)"); menu_index++;
            param.Add(menu_index, "isToken (bool)"); menu_index++;
            level3_menus.Add("Login", new Dictionary<int, string>(param));
            param.Clear();

            menu_index = 1;
            param.Add(menu_index, "No params"); menu_index++;
            level3_menus.Add("Logout", new Dictionary<int, string>(param));
            param.Clear();

            menu_index = 1;
            param.Add(menu_index, "No params"); menu_index++;
            level3_menus.Add("CurrentUsername", new Dictionary<int, string>(param));
            param.Clear();

            menu_index = 1;
            param.Add(menu_index, "No params"); menu_index++;
            level3_menus.Add("IsConnected", new Dictionary<int, string>(param));
            param.Clear();

            menu_index = 1;
            param.Add(menu_index, "No params"); menu_index++;
            level3_menus.Add("IsLoggedIn", new Dictionary<int, string>(param));
            param.Clear();

            menu_index = 1;
            param.Add(menu_index, "No params"); menu_index++;
            level3_menus.Add("AccessToken", new Dictionary<int, string>(param));
            param.Clear();

            menu_index = 1;
            param.Add(menu_index, "username (string)"); menu_index++;
            param.Add(menu_index, "token (string)"); menu_index++;
            level3_menus.Add("LoginWithAgoraToken", new Dictionary<int, string>(param));
            param.Clear();

            menu_index = 1;
            param.Add(menu_index, "token (string)"); menu_index++;
            level3_menus.Add("RenewAgoraToken", new Dictionary<int, string>(param));
            param.Clear();
        }

        internal void InitLevel2Menus_IChatManager()
        {
            Dictionary<int, string> functions_IChatManager = new Dictionary<int, string>();
            int menu_index = 1;
            functions_IChatManager.Add(menu_index, "DeleteConversation"); menu_index++;
            functions_IChatManager.Add(menu_index, "DownloadAttachment"); menu_index++;
            functions_IChatManager.Add(menu_index, "DownloadThumbnail"); menu_index++;
            functions_IChatManager.Add(menu_index, "FetchHistoryMessagesFromServer"); menu_index++;
            functions_IChatManager.Add(menu_index, "GetConversation"); menu_index++;
            functions_IChatManager.Add(menu_index, "GetConversationsFromServer"); menu_index++;
            functions_IChatManager.Add(menu_index, "GetUnreadMessageCount"); menu_index++;
            functions_IChatManager.Add(menu_index, "ImportMessages"); menu_index++;
            functions_IChatManager.Add(menu_index, "LoadAllConversations"); menu_index++;
            functions_IChatManager.Add(menu_index, "LoadMessage"); menu_index++;
            functions_IChatManager.Add(menu_index, "MarkAllConversationsAsRead"); menu_index++;
            functions_IChatManager.Add(menu_index, "RecallMessage"); menu_index++;
            functions_IChatManager.Add(menu_index, "ResendMessage"); menu_index++;
            functions_IChatManager.Add(menu_index, "SearchMsgFromDB"); menu_index++;
            functions_IChatManager.Add(menu_index, "SendConversationReadAck"); menu_index++;
            functions_IChatManager.Add(menu_index, "SendTxtMessage"); menu_index++;
            functions_IChatManager.Add(menu_index, "SendImageMessage"); menu_index++;
            functions_IChatManager.Add(menu_index, "SendFileMessage"); menu_index++;
            functions_IChatManager.Add(menu_index, "SendVideoMessage"); menu_index++;
            functions_IChatManager.Add(menu_index, "SendVoiceMessage"); menu_index++;
            functions_IChatManager.Add(menu_index, "SendCmdMessage"); menu_index++;
            functions_IChatManager.Add(menu_index, "SendCustomMessage"); menu_index++;
            functions_IChatManager.Add(menu_index, "SendLocationMessage"); menu_index++;
            functions_IChatManager.Add(menu_index, "SendMessageReadAck"); menu_index++;
            functions_IChatManager.Add(menu_index, "ChatManagerUpdateMessage"); menu_index++;
            functions_IChatManager.Add(menu_index, "RemoveMessagesBeforeTimestamp"); menu_index++;
            functions_IChatManager.Add(menu_index, "DeleteConversationFromServer"); menu_index++;
            level2_menus.Add("IChatManager", functions_IChatManager);
        }

        internal void InitLevel3Menus_IChatManager()
        {
            Dictionary<int, string> param = new Dictionary<int, string>();
            int menu_index = 1;
            param.Add(menu_index, "conversationId (string)"); menu_index++;
            level3_menus.Add("DeleteConversation", new Dictionary<int, string>(param));
            param.Clear();

            menu_index = 1;
            param.Add(menu_index, "messageId (string)"); menu_index++;
            level3_menus.Add("DownloadAttachment", new Dictionary<int, string>(param));
            param.Clear();

            menu_index = 1;
            param.Add(menu_index, "messageId (string)"); menu_index++;
            level3_menus.Add("DownloadThumbnail", new Dictionary<int, string>(param));
            param.Clear();

            menu_index = 1;
            param.Add(menu_index, "conversationId (string)"); menu_index++;
            param.Add(menu_index, "conversationType (0:Chat, 1:Group, 2:Room)"); menu_index++;
            param.Add(menu_index, "startMessageId (string, default is empty string)"); menu_index++;
            param.Add(menu_index, "count (int)"); menu_index++;
            level3_menus.Add("FetchHistoryMessagesFromServer", new Dictionary<int, string>(param));
            param.Clear();

            menu_index = 1;
            param.Add(menu_index, "conversationId (string)"); menu_index++;
            param.Add(menu_index, "conversationType (0:Chat, 1:Group, 2:Room)"); menu_index++;
            param.Add(menu_index, "createIfNeed (bool)"); menu_index++;
            level3_menus.Add("GetConversation", new Dictionary<int, string>(param));
            param.Clear();

            menu_index = 1;
            param.Add(menu_index, "No params"); menu_index++;
            level3_menus.Add("GetConversationsFromServer", new Dictionary<int, string>(param));
            param.Clear();

            menu_index = 1;
            param.Add(menu_index, "No params"); menu_index++;
            level3_menus.Add("GetUnreadMessageCount", new Dictionary<int, string>(param));
            param.Clear();

            menu_index = 1;
            param.Add(menu_index, "No params"); menu_index++;
            level3_menus.Add("ImportMessages", new Dictionary<int, string>(param));
            param.Clear();

            menu_index = 1;
            param.Add(menu_index, "No params"); menu_index++;
            level3_menus.Add("LoadAllConversations", new Dictionary<int, string>(param));
            param.Clear();

            menu_index = 1;
            param.Add(menu_index, "messageId (string)"); menu_index++;
            level3_menus.Add("LoadMessage", new Dictionary<int, string>(param));
            param.Clear();

            menu_index = 1;
            param.Add(menu_index, "No params"); menu_index++;
            level3_menus.Add("MarkAllConversationsAsRead", new Dictionary<int, string>(param));
            param.Clear();

            menu_index = 1;
            param.Add(menu_index, "messageId (string)"); menu_index++;
            level3_menus.Add("RecallMessage", new Dictionary<int, string>(param));
            param.Clear();

            menu_index = 1;
            param.Add(menu_index, "messageId (string)"); menu_index++;
            level3_menus.Add("ResendMessage", new Dictionary<int, string>(param));
            param.Clear();

            menu_index = 1;
            param.Add(menu_index, "keywords (string)"); menu_index++;
            param.Add(menu_index, "timestamp (long)"); menu_index++;
            param.Add(menu_index, "maxCount (int)"); menu_index++;
            param.Add(menu_index, "from (string)"); menu_index++;
            param.Add(menu_index, "direction (0: Up, 1: Down)"); menu_index++;
            level3_menus.Add("SearchMsgFromDB", new Dictionary<int, string>(param));
            param.Clear();

            menu_index = 1;
            param.Add(menu_index, "conversationId (string)"); menu_index++;
            level3_menus.Add("SendConversationReadAck", new Dictionary<int, string>(param));
            param.Clear();

            menu_index = 1;
            param.Add(menu_index, "to (string)"); menu_index++;
            param.Add(menu_index, "text (string)"); menu_index++;
            level3_menus.Add("SendTxtMessage", new Dictionary<int, string>(param));
            param.Clear();

            menu_index = 1;
            param.Add(menu_index, "to (string)"); menu_index++;
            param.Add(menu_index, "path (string: D:\\a\\b\\c )"); menu_index++;
            level3_menus.Add("SendImageMessage", new Dictionary<int, string>(param));
            param.Clear();

            menu_index = 1;
            param.Add(menu_index, "to (string)"); menu_index++;
            param.Add(menu_index, "path(string: D:\\a\\b\\c)"); menu_index++;
            param.Add(menu_index, "filesize (int)"); menu_index++;
            level3_menus.Add("SendFileMessage", new Dictionary<int, string>(param));
            param.Clear();

            menu_index = 1;
            param.Add(menu_index, "to (string)"); menu_index++;
            param.Add(menu_index, "path(string: D:\\a\\b\\c)"); menu_index++;
            level3_menus.Add("SendVideoMessage", new Dictionary<int, string>(param));
            param.Clear();

            menu_index = 1;
            param.Add(menu_index, "to (string)"); menu_index++;
            param.Add(menu_index, "path(string: D:\\a\\b\\c)"); menu_index++;
            level3_menus.Add("SendVoiceMessage", new Dictionary<int, string>(param));
            param.Clear();

            menu_index = 1;
            param.Add(menu_index, "to (string)"); menu_index++;
            param.Add(menu_index, "action (string)"); menu_index++;
            level3_menus.Add("SendCmdMessage", new Dictionary<int, string>(param));
            param.Clear();

            menu_index = 1;
            param.Add(menu_index, "to (string)"); menu_index++;
            param.Add(menu_index, "customEvent (string)"); menu_index++;
            level3_menus.Add("SendCustomMessage", new Dictionary<int, string>(param));
            param.Clear();

            menu_index = 1;
            param.Add(menu_index, "to (string)"); menu_index++;
            param.Add(menu_index, "address (string)"); menu_index++;
            param.Add(menu_index, "buildingName (string)"); menu_index++;
            level3_menus.Add("SendLocationMessage", new Dictionary<int, string>(param));
            param.Clear();

            menu_index = 1;
            param.Add(menu_index, "messageId (string)"); menu_index++;
            level3_menus.Add("SendMessageReadAck", new Dictionary<int, string>(param));
            param.Clear();

            menu_index = 1;
            param.Add(menu_index, "messageId (string)"); menu_index++;
            param.Add(menu_index, "status (int, 0:CREATE, 1:PROGRESS, 2:SUCCESS, 3:FAIL)"); menu_index++;
            level3_menus.Add("ChatManagerUpdateMessage", new Dictionary<int, string>(param));
            param.Clear();

            menu_index = 1;
            param.Add(menu_index, "timeStamp (long)"); menu_index++;
            level3_menus.Add("RemoveMessagesBeforeTimestamp", new Dictionary<int, string>(param));
            param.Clear();

            menu_index = 1;
            param.Add(menu_index, "conversationId (string)"); menu_index++;
            param.Add(menu_index, "conversationType (0:Chat, 1:Group, 2:Room)"); menu_index++;
            param.Add(menu_index, "isDeleteServerMessages (int 0:false; 1:true)"); menu_index++;
            level3_menus.Add("DeleteConversationFromServer", new Dictionary<int, string>(param));
            param.Clear();
        }

        internal void InitLevel2Menus_IContactManager()
        {
            Dictionary<int, string> functions_IContactManager = new Dictionary<int, string>();
            int menu_index = 1;
            functions_IContactManager.Add(menu_index, "AddContact"); menu_index++;
            functions_IContactManager.Add(menu_index, "DeleteContact"); menu_index++;
            functions_IContactManager.Add(menu_index, "GetAllContactsFromServer"); menu_index++;
            functions_IContactManager.Add(menu_index, "GetAllContactsFromDB"); menu_index++;
            functions_IContactManager.Add(menu_index, "AddUserToBlockList"); menu_index++;
            functions_IContactManager.Add(menu_index, "RemoveUserFromBlockList"); menu_index++;
            functions_IContactManager.Add(menu_index, "GetBlockListFromServer"); menu_index++;
            functions_IContactManager.Add(menu_index, "AcceptInvitation"); menu_index++;
            functions_IContactManager.Add(menu_index, "DeclineInvitation"); menu_index++;
            functions_IContactManager.Add(menu_index, "GetSelfIdsOnOtherPlatform"); menu_index++;
            level2_menus.Add("IContactManager", functions_IContactManager);
        }

        internal void InitLevel3Menus_IContactManager()
        {
            Dictionary<int, string> param = new Dictionary<int, string>();
            int menu_index = 1;
            param.Add(menu_index, "username (string)"); menu_index++;
            param.Add(menu_index, "reason (string)"); menu_index++;
            level3_menus.Add("AddContact", new Dictionary<int, string>(param));
            param.Clear();

            menu_index = 1;
            param.Add(menu_index, "username (string)"); menu_index++;
            param.Add(menu_index, "keepConversation (int; 0:false; 1:true)"); menu_index++;
            level3_menus.Add("DeleteContact", new Dictionary<int, string>(param));
            param.Clear();

            menu_index = 1;
            param.Add(menu_index, "No params"); menu_index++;
            level3_menus.Add("GetAllContactsFromServer", new Dictionary<int, string>(param));
            param.Clear();

            menu_index = 1;
            param.Add(menu_index, "No params"); menu_index++;
            level3_menus.Add("GetAllContactsFromDB", new Dictionary<int, string>(param));
            param.Clear();

            menu_index = 1;
            param.Add(menu_index, "username (string)"); menu_index++;
            level3_menus.Add("AddUserToBlockList", new Dictionary<int, string>(param));
            param.Clear();

            menu_index = 1;
            param.Add(menu_index, "username (string)"); menu_index++;
            level3_menus.Add("RemoveUserFromBlockList", new Dictionary<int, string>(param));
            param.Clear();

            menu_index = 1;
            param.Add(menu_index, "No params"); menu_index++;
            level3_menus.Add("GetBlockListFromServer", new Dictionary<int, string>(param));
            param.Clear();

            menu_index = 1;
            param.Add(menu_index, "username (string)"); menu_index++;
            level3_menus.Add("AcceptInvitation", new Dictionary<int, string>(param));
            param.Clear();

            menu_index = 1;
            param.Add(menu_index, "username (string)"); menu_index++;
            level3_menus.Add("DeclineInvitation", new Dictionary<int, string>(param));
            param.Clear();

            menu_index = 1;
            param.Add(menu_index, "No params"); menu_index++;
            level3_menus.Add("GetSelfIdsOnOtherPlatform", new Dictionary<int, string>(param));
            param.Clear();
        }

        internal void InitLevel2Menus_IConversationManager()
        {
            Dictionary<int, string> functions_IConversationManager = new Dictionary<int, string>();
            int menu_index = 1;
            functions_IConversationManager.Add(menu_index, "LastMessage"); menu_index++;
            functions_IConversationManager.Add(menu_index, "LastReceivedMessage"); menu_index++;
            functions_IConversationManager.Add(menu_index, "GetExt"); menu_index++;
            functions_IConversationManager.Add(menu_index, "SetExt"); menu_index++;
            functions_IConversationManager.Add(menu_index, "UnReadCount"); menu_index++;
            functions_IConversationManager.Add(menu_index, "MessagesCount"); menu_index++;
            functions_IConversationManager.Add(menu_index, "MarkMessageAsRead"); menu_index++;
            functions_IConversationManager.Add(menu_index, "MarkAllMessageAsRead"); menu_index++;
            functions_IConversationManager.Add(menu_index, "InsertMessage"); menu_index++;
            functions_IConversationManager.Add(menu_index, "AppendMessage"); menu_index++;
            functions_IConversationManager.Add(menu_index, "UpdateMessage"); menu_index++;
            functions_IConversationManager.Add(menu_index, "DeleteMessage"); menu_index++;
            functions_IConversationManager.Add(menu_index, "DeleteAllMessages"); menu_index++;
            functions_IConversationManager.Add(menu_index, "LoadConverationMessage"); menu_index++;
            functions_IConversationManager.Add(menu_index, "LoadMessagesWithMsgType"); menu_index++;
            functions_IConversationManager.Add(menu_index, "LoadMessages"); menu_index++;
            functions_IConversationManager.Add(menu_index, "LoadMessagesWithKeyword"); menu_index++;
            functions_IConversationManager.Add(menu_index, "LoadMessagesWithTime"); menu_index++;
            level2_menus.Add("IConversationManager", functions_IConversationManager);
        }

        internal void InitLevel3Menus_IConversationManager()
        {
            Dictionary<int, string> param = new Dictionary<int, string>();
            int menu_index = 1;
            param.Add(menu_index, "conversationId (string)"); menu_index++;
            param.Add(menu_index, "conversationType (0:Chat, 1:Group, 2:Room)"); menu_index++;
            level3_menus.Add("LastMessage", new Dictionary<int, string>(param));
            param.Clear();

            menu_index = 1;
            param.Add(menu_index, "conversationId (string)"); menu_index++;
            param.Add(menu_index, "conversationType (0:Chat, 1:Group, 2:Room)"); menu_index++;
            level3_menus.Add("LastReceivedMessage", new Dictionary<int, string>(param));
            param.Clear();

            menu_index = 1;
            param.Add(menu_index, "conversationId (string)"); menu_index++;
            param.Add(menu_index, "conversationType (0:Chat, 1:Group, 2:Room)"); menu_index++;
            level3_menus.Add("GetExt", new Dictionary<int, string>(param));
            param.Clear();

            menu_index = 1;
            param.Add(menu_index, "conversationId (string)"); menu_index++;
            param.Add(menu_index, "conversationType (0:Chat, 1:Group, 2:Room)"); menu_index++;
            level3_menus.Add("SetExt", new Dictionary<int, string>(param));
            param.Clear();

            menu_index = 1;
            param.Add(menu_index, "conversationId (string)"); menu_index++;
            param.Add(menu_index, "conversationType (0:Chat, 1:Group, 2:Room)"); menu_index++;
            level3_menus.Add("UnReadCount", new Dictionary<int, string>(param));
            param.Clear();

            menu_index = 1;
            param.Add(menu_index, "conversationId (string)"); menu_index++;
            param.Add(menu_index, "conversationType (0:Chat, 1:Group, 2:Room)"); menu_index++;
            level3_menus.Add("MessagesCount", new Dictionary<int, string>(param));
            param.Clear();

            menu_index = 1;
            param.Add(menu_index, "conversationId (string)"); menu_index++;
            param.Add(menu_index, "conversationType (0:Chat, 1:Group, 2:Room)"); menu_index++;
            param.Add(menu_index, "messageId (string)"); menu_index++;
            level3_menus.Add("MarkMessageAsRead", new Dictionary<int, string>(param));
            param.Clear();

            menu_index = 1;
            param.Add(menu_index, "conversationId (string)"); menu_index++;
            param.Add(menu_index, "conversationType (0:Chat, 1:Group, 2:Room)"); menu_index++;
            level3_menus.Add("MarkAllMessageAsRead", new Dictionary<int, string>(param));
            param.Clear();

            menu_index = 1;
            param.Add(menu_index, "conversationId (string)"); menu_index++;
            param.Add(menu_index, "conversationType (0:Chat, 1:Group, 2:Room)"); menu_index++;
            level3_menus.Add("InsertMessage", new Dictionary<int, string>(param));
            param.Clear();

            menu_index = 1;
            param.Add(menu_index, "conversationId (string)"); menu_index++;
            param.Add(menu_index, "conversationType (0:Chat, 1:Group, 2:Room)"); menu_index++;
            level3_menus.Add("AppendMessage", new Dictionary<int, string>(param));
            param.Clear();

            menu_index = 1;
            param.Add(menu_index, "conversationId (string)"); menu_index++;
            param.Add(menu_index, "conversationType (0:Chat, 1:Group, 2:Room)"); menu_index++;
            param.Add(menu_index, "messageId (string)"); menu_index++;
            param.Add(menu_index, "status (0:create; 1:progress; 2:success; 3:fail)"); menu_index++;
            level3_menus.Add("UpdateMessage", new Dictionary<int, string>(param));
            param.Clear();

            menu_index = 1;
            param.Add(menu_index, "conversationId (string)"); menu_index++;
            param.Add(menu_index, "conversationType (0:Chat, 1:Group, 2:Room)"); menu_index++;
            param.Add(menu_index, "messageId (string)"); menu_index++;
            level3_menus.Add("DeleteMessage", new Dictionary<int, string>(param));
            param.Clear();

            menu_index = 1;
            param.Add(menu_index, "conversationId (string)"); menu_index++;
            param.Add(menu_index, "conversationType (0:Chat, 1:Group, 2:Room)"); menu_index++;
            level3_menus.Add("DeleteAllMessages", new Dictionary<int, string>(param));
            param.Clear();

            menu_index = 1;
            param.Add(menu_index, "conversationId (string)"); menu_index++;
            param.Add(menu_index, "conversationType (0:Chat, 1:Group, 2:Room)"); menu_index++;
            param.Add(menu_index, "messageId (string)"); menu_index++;
            level3_menus.Add("LoadConverationMessage", new Dictionary<int, string>(param));
            param.Clear();

            menu_index = 1;
            param.Add(menu_index, "conversationId (string)"); menu_index++;
            param.Add(menu_index, "conversationType (0:Chat, 1:Group, 2:Room)"); menu_index++;
            param.Add(menu_index, "bodyType (0:Txt,1:Image;2:Video;3:Location;4:Voice;5:File:6:Cmd;7:Custom)"); menu_index++;
            param.Add(menu_index, "sender (string)"); menu_index++;
            param.Add(menu_index, "timestamp (long)"); menu_index++;
            param.Add(menu_index, "count (int)"); menu_index++;
            param.Add(menu_index, "direction (0:up; 1:down)"); menu_index++;
            level3_menus.Add("LoadMessagesWithMsgType", new Dictionary<int, string>(param));
            param.Clear();

            menu_index = 1;
            param.Add(menu_index, "conversationId (string)"); menu_index++;
            param.Add(menu_index, "conversationType (0:Chat, 1:Group, 2:Room)"); menu_index++;
            param.Add(menu_index, "startMessageId (string)"); menu_index++;
            param.Add(menu_index, "count (int)"); menu_index++;
            param.Add(menu_index, "direction (0:up; 1:down)"); menu_index++;
            level3_menus.Add("LoadMessages", new Dictionary<int, string>(param));
            param.Clear();

            menu_index = 1;
            param.Add(menu_index, "conversationId (string)"); menu_index++;
            param.Add(menu_index, "conversationType (0:Chat, 1:Group, 2:Room)"); menu_index++;
            param.Add(menu_index, "keywords (string)"); menu_index++;
            param.Add(menu_index, "sender (string)"); menu_index++;
            param.Add(menu_index, "timestamp (long)"); menu_index++;
            param.Add(menu_index, "count (int)"); menu_index++;
            param.Add(menu_index, "timestamp (long)"); menu_index++;
            param.Add(menu_index, "direction (0:up; 1:down)"); menu_index++;
            level3_menus.Add("LoadMessagesWithKeyword", new Dictionary<int, string>(param));
            param.Clear();

            menu_index = 1;
            param.Add(menu_index, "conversationId (string)"); menu_index++;
            param.Add(menu_index, "conversationType (0:Chat, 1:Group, 2:Room)"); menu_index++;
            param.Add(menu_index, "startTime (long)"); menu_index++;
            param.Add(menu_index, "endTime (long)"); menu_index++;
            param.Add(menu_index, "count (int)"); menu_index++;
            level3_menus.Add("LoadMessagesWithTime", new Dictionary<int, string>(param));
            param.Clear();
        }

        internal void InitLevel2Menus_IGroupManager()
        {
            Dictionary<int, string> functions_IGroupManager = new Dictionary<int, string>();
            int menu_index = 1;
            functions_IGroupManager.Add(menu_index, "applyJoinToGroup"); menu_index++;
            functions_IGroupManager.Add(menu_index, "AcceptGroupInvitation"); menu_index++;
            functions_IGroupManager.Add(menu_index, "AcceptGroupJoinApplication"); menu_index++;
            functions_IGroupManager.Add(menu_index, "AddGroupAdmin"); menu_index++;
            functions_IGroupManager.Add(menu_index, "AddGroupMembers"); menu_index++;
            functions_IGroupManager.Add(menu_index, "AddGroupWhiteList"); menu_index++;
            functions_IGroupManager.Add(menu_index, "BlockGroup"); menu_index++;
            functions_IGroupManager.Add(menu_index, "BlockGroupMembers"); menu_index++;
            functions_IGroupManager.Add(menu_index, "ChangeGroupDescription"); menu_index++;
            functions_IGroupManager.Add(menu_index, "ChangeGroupName"); menu_index++;
            functions_IGroupManager.Add(menu_index, "ChangeGroupOwner"); menu_index++;
            functions_IGroupManager.Add(menu_index, "CheckIfInGroupWhiteList"); menu_index++;
            functions_IGroupManager.Add(menu_index, "CreateGroup"); menu_index++;
            functions_IGroupManager.Add(menu_index, "DeclineGroupInvitation"); menu_index++;
            functions_IGroupManager.Add(menu_index, "DeclineGroupJoinApplication"); menu_index++;
            functions_IGroupManager.Add(menu_index, "DestroyGroup"); menu_index++;
            functions_IGroupManager.Add(menu_index, "DownloadGroupSharedFile"); menu_index++;
            functions_IGroupManager.Add(menu_index, "GetGroupAnnouncementFromServer"); menu_index++;
            functions_IGroupManager.Add(menu_index, "GetGroupBlockListFromServer"); menu_index++;
            functions_IGroupManager.Add(menu_index, "GetGroupFileListFromServer"); menu_index++;
            functions_IGroupManager.Add(menu_index, "GetGroupMemberListFromServer"); menu_index++;
            functions_IGroupManager.Add(menu_index, "GetGroupMuteListFromServer"); menu_index++;
            functions_IGroupManager.Add(menu_index, "GetGroupSpecificationFromServer"); menu_index++;
            functions_IGroupManager.Add(menu_index, "GetGroupWhiteListFromServer"); menu_index++;
            functions_IGroupManager.Add(menu_index, "GetGroupWithId"); menu_index++;
            functions_IGroupManager.Add(menu_index, "GetJoinedGroups"); menu_index++;
            functions_IGroupManager.Add(menu_index, "FetchJoinedGroupsFromServer"); menu_index++;
            functions_IGroupManager.Add(menu_index, "FetchPublicGroupsFromServer"); menu_index++;
            functions_IGroupManager.Add(menu_index, "JoinPublicGroup"); menu_index++;
            functions_IGroupManager.Add(menu_index, "LeaveGroup"); menu_index++;
            functions_IGroupManager.Add(menu_index, "MuteGroupAllMembers"); menu_index++;
            functions_IGroupManager.Add(menu_index, "MuteGroupMembers"); menu_index++;
            functions_IGroupManager.Add(menu_index, "RemoveGroupAdmin"); menu_index++;
            functions_IGroupManager.Add(menu_index, "DeleteGroupSharedFile"); menu_index++;
            functions_IGroupManager.Add(menu_index, "DeleteGroupMembers"); menu_index++;
            functions_IGroupManager.Add(menu_index, "RemoveGroupWhiteList"); menu_index++;
            functions_IGroupManager.Add(menu_index, "UnBlockGroup"); menu_index++;
            functions_IGroupManager.Add(menu_index, "UnBlockGroupMembers"); menu_index++;
            functions_IGroupManager.Add(menu_index, "UnMuteGroupAllMembers"); menu_index++;
            functions_IGroupManager.Add(menu_index, "UnMuteGroupMembers"); menu_index++;
            functions_IGroupManager.Add(menu_index, "UpdateGroupAnnouncement"); menu_index++;
            functions_IGroupManager.Add(menu_index, "UpdateGroupExt"); menu_index++;
            functions_IGroupManager.Add(menu_index, "UploadGroupSharedFile"); menu_index++;
            level2_menus.Add("IGroupManager", functions_IGroupManager);
        }

        internal void InitLevel3Menus_IGroupManager()
        {
            Dictionary<int, string> param = new Dictionary<int, string>();
            int menu_index = 1;
            param.Add(menu_index, "groupId (string)"); menu_index++;
            param.Add(menu_index, "reason (string)"); menu_index++;
            level3_menus.Add("applyJoinToGroup", new Dictionary<int, string>(param));
            param.Clear();

            menu_index = 1;
            param.Add(menu_index, "groupId (string)"); menu_index++;
            level3_menus.Add("AcceptGroupInvitation", new Dictionary<int, string>(param));
            param.Clear();

            menu_index = 1;
            param.Add(menu_index, "groupId (string)"); menu_index++;
            param.Add(menu_index, "username (string)"); menu_index++;
            level3_menus.Add("AcceptGroupJoinApplication", new Dictionary<int, string>(param));
            param.Clear();

            menu_index = 1;
            param.Add(menu_index, "groupId (string)"); menu_index++;
            param.Add(menu_index, "memberId (string)"); menu_index++;
            level3_menus.Add("AddGroupAdmin", new Dictionary<int, string>(param));
            param.Clear();

            menu_index = 1;
            param.Add(menu_index, "groupId (string)"); menu_index++;
            param.Add(menu_index, "memberId1 (string)"); menu_index++;
            param.Add(menu_index, "memberId2 (string)"); menu_index++;
            param.Add(menu_index, "memberId3 (string)"); menu_index++;
            level3_menus.Add("AddGroupMembers", new Dictionary<int, string>(param));
            param.Clear();

            menu_index = 1;
            param.Add(menu_index, "groupId (string)"); menu_index++;
            param.Add(menu_index, "memberId1 (string)"); menu_index++;
            param.Add(menu_index, "memberId2 (string)"); menu_index++;
            param.Add(menu_index, "memberId3 (string)"); menu_index++;
            level3_menus.Add("AddGroupWhiteList", new Dictionary<int, string>(param));
            param.Clear();

            menu_index = 1;
            param.Add(menu_index, "groupId (string)"); menu_index++;
            level3_menus.Add("BlockGroup", new Dictionary<int, string>(param));
            param.Clear();

            menu_index = 1;
            param.Add(menu_index, "groupId (string)"); menu_index++;
            param.Add(menu_index, "memberId1 (string)"); menu_index++;
            param.Add(menu_index, "memberId2 (string)"); menu_index++;
            param.Add(menu_index, "memberId3 (string)"); menu_index++;
            level3_menus.Add("BlockGroupMembers", new Dictionary<int, string>(param));
            param.Clear();

            menu_index = 1;
            param.Add(menu_index, "groupId (string)"); menu_index++;
            param.Add(menu_index, "desc (string)"); menu_index++;
            level3_menus.Add("ChangeGroupDescription", new Dictionary<int, string>(param));
            param.Clear();

            menu_index = 1;
            param.Add(menu_index, "groupId (string)"); menu_index++;
            param.Add(menu_index, "name (string)"); menu_index++;
            level3_menus.Add("ChangeGroupName", new Dictionary<int, string>(param));
            param.Clear();

            menu_index = 1;
            param.Add(menu_index, "groupId (string)"); menu_index++;
            param.Add(menu_index, "newOwner (string)"); menu_index++;
            level3_menus.Add("ChangeGroupOwner", new Dictionary<int, string>(param));
            param.Clear();

            menu_index = 1;
            param.Add(menu_index, "groupId (string)"); menu_index++;
            level3_menus.Add("CheckIfInGroupWhiteList", new Dictionary<int, string>(param));
            param.Clear();

            menu_index = 1;
            param.Add(menu_index, "groupName (string)"); menu_index++;
            param.Add(menu_index, "desc (string)"); menu_index++;
            param.Add(menu_index, "memberId1 (string)"); menu_index++;
            param.Add(menu_index, "memberId2 (string)"); menu_index++;
            param.Add(menu_index, "inviteReason (string)"); menu_index++;
            level3_menus.Add("CreateGroup", new Dictionary<int, string>(param));
            param.Clear();

            menu_index = 1;
            param.Add(menu_index, "groupId (string)"); menu_index++;
            param.Add(menu_index, "reason (string)"); menu_index++;
            level3_menus.Add("DeclineGroupInvitation", new Dictionary<int, string>(param));
            param.Clear();

            menu_index = 1;
            param.Add(menu_index, "groupId (string)"); menu_index++;
            param.Add(menu_index, "username (string)"); menu_index++;
            param.Add(menu_index, "reason (string)"); menu_index++;
            level3_menus.Add("DeclineGroupJoinApplication", new Dictionary<int, string>(param));
            param.Clear();

            menu_index = 1;
            param.Add(menu_index, "groupId (string)"); menu_index++;
            level3_menus.Add("DestroyGroup", new Dictionary<int, string>(param));
            param.Clear();

            menu_index = 1;
            param.Add(menu_index, "groupId (string)"); menu_index++;
            param.Add(menu_index, "fileId (string)"); menu_index++;
            param.Add(menu_index, "savePath (string d:\\a\\b\\c)"); menu_index++;
            level3_menus.Add("DownloadGroupSharedFile", new Dictionary<int, string>(param));
            param.Clear();

            menu_index = 1;
            param.Add(menu_index, "groupId (string)"); menu_index++;
            level3_menus.Add("GetGroupAnnouncementFromServer", new Dictionary<int, string>(param));
            param.Clear();

            menu_index = 1;
            param.Add(menu_index, "groupId (string)"); menu_index++;
            param.Add(menu_index, "pageNum (int)"); menu_index++;
            param.Add(menu_index, "pageSize (int)"); menu_index++;
            level3_menus.Add("GetGroupBlockListFromServer", new Dictionary<int, string>(param));
            param.Clear();

            menu_index = 1;
            param.Add(menu_index, "groupId (string)"); menu_index++;
            param.Add(menu_index, "pageNum (int)"); menu_index++;
            param.Add(menu_index, "pageSize (int)"); menu_index++;
            level3_menus.Add("GetGroupFileListFromServer", new Dictionary<int, string>(param));
            param.Clear();

            menu_index = 1;
            param.Add(menu_index, "groupId (string)"); menu_index++;
            param.Add(menu_index, "pageSize (int)"); menu_index++;
            param.Add(menu_index, "cursor (string)"); menu_index++;
            level3_menus.Add("GetGroupMemberListFromServer", new Dictionary<int, string>(param));
            param.Clear();

            menu_index = 1;
            param.Add(menu_index, "groupId (string)"); menu_index++;
            param.Add(menu_index, "pageNum (int)"); menu_index++;
            param.Add(menu_index, "pageSize (int)"); menu_index++;
            level3_menus.Add("GetGroupMuteListFromServer", new Dictionary<int, string>(param));
            param.Clear();

            menu_index = 1;
            param.Add(menu_index, "groupId (string)"); menu_index++;
            level3_menus.Add("GetGroupSpecificationFromServer", new Dictionary<int, string>(param));
            param.Clear();

            menu_index = 1;
            param.Add(menu_index, "groupId (string)"); menu_index++;
            level3_menus.Add("GetGroupWhiteListFromServer", new Dictionary<int, string>(param));
            param.Clear();

            menu_index = 1;
            param.Add(menu_index, "groupId (string)"); menu_index++;
            level3_menus.Add("GetGroupWithId", new Dictionary<int, string>(param));
            param.Clear();

            menu_index = 1;
            param.Add(menu_index, "No params"); menu_index++;
            level3_menus.Add("GetJoinedGroups", new Dictionary<int, string>(param));
            param.Clear();

            menu_index = 1;
            param.Add(menu_index, "pageNum (int)"); menu_index++;
            param.Add(menu_index, "pageSize (int)"); menu_index++;
            level3_menus.Add("FetchJoinedGroupsFromServer", new Dictionary<int, string>(param));
            param.Clear();

            menu_index = 1;
            param.Add(menu_index, "pageSize (int)"); menu_index++;
            param.Add(menu_index, "cursor (string)"); menu_index++;
            level3_menus.Add("FetchPublicGroupsFromServer", new Dictionary<int, string>(param));
            param.Clear();

            menu_index = 1;
            param.Add(menu_index, "groupId (string)"); menu_index++;
            level3_menus.Add("JoinPublicGroup", new Dictionary<int, string>(param));
            param.Clear();

            menu_index = 1;
            param.Add(menu_index, "groupId (string)"); menu_index++;
            level3_menus.Add("LeaveGroup", new Dictionary<int, string>(param));
            param.Clear();

            menu_index = 1;
            param.Add(menu_index, "groupId (string)"); menu_index++;
            level3_menus.Add("MuteGroupAllMembers", new Dictionary<int, string>(param));
            param.Clear();

            menu_index = 1;
            param.Add(menu_index, "groupId (string)"); menu_index++;
            param.Add(menu_index, "memberId1 (string)"); menu_index++;
            param.Add(menu_index, "memberId2 (string)"); menu_index++;
            level3_menus.Add("MuteGroupMembers", new Dictionary<int, string>(param));
            param.Clear();

            menu_index = 1;
            param.Add(menu_index, "groupId (string)"); menu_index++;
            param.Add(menu_index, "memberId (string)"); menu_index++;
            level3_menus.Add("RemoveGroupAdmin", new Dictionary<int, string>(param));
            param.Clear();

            menu_index = 1;
            param.Add(menu_index, "groupId (string)"); menu_index++;
            param.Add(menu_index, "fileId (string)"); menu_index++;
            level3_menus.Add("DeleteGroupSharedFile", new Dictionary<int, string>(param));
            param.Clear();

            menu_index = 1;
            param.Add(menu_index, "groupId (string)"); menu_index++;
            param.Add(menu_index, "memberId1 (string)"); menu_index++;
            param.Add(menu_index, "memberId2 (string)"); menu_index++;
            level3_menus.Add("DeleteGroupMembers", new Dictionary<int, string>(param));
            param.Clear();

            menu_index = 1;
            param.Add(menu_index, "groupId (string)"); menu_index++;
            param.Add(menu_index, "memberId1 (string)"); menu_index++;
            param.Add(menu_index, "memberId2 (string)"); menu_index++;
            level3_menus.Add("RemoveGroupWhiteList", new Dictionary<int, string>(param));
            param.Clear();

            menu_index = 1;
            param.Add(menu_index, "groupId (string)"); menu_index++;
            level3_menus.Add("UnBlockGroup", new Dictionary<int, string>(param));
            param.Clear();

            menu_index = 1;
            param.Add(menu_index, "groupId (string)"); menu_index++;
            param.Add(menu_index, "memberId1 (string)"); menu_index++;
            param.Add(menu_index, "memberId2 (string)"); menu_index++;
            level3_menus.Add("UnBlockGroupMembers", new Dictionary<int, string>(param));
            param.Clear();

            menu_index = 1;
            param.Add(menu_index, "groupId (string)"); menu_index++;
            level3_menus.Add("UnMuteGroupAllMembers", new Dictionary<int, string>(param));
            param.Clear();

            menu_index = 1;
            param.Add(menu_index, "groupId (string)"); menu_index++;
            param.Add(menu_index, "memberId1 (string)"); menu_index++;
            param.Add(menu_index, "memberId2 (string)"); menu_index++;
            level3_menus.Add("UnMuteGroupMembers", new Dictionary<int, string>(param));
            param.Clear();

            menu_index = 1;
            param.Add(menu_index, "groupId (string)"); menu_index++;
            param.Add(menu_index, "announcement (string)"); menu_index++;
            level3_menus.Add("UpdateGroupAnnouncement", new Dictionary<int, string>(param));
            param.Clear();

            menu_index = 1;
            param.Add(menu_index, "groupId (string)"); menu_index++;
            param.Add(menu_index, "ext (string)"); menu_index++;
            level3_menus.Add("UpdateGroupExt", new Dictionary<int, string>(param));
            param.Clear();

            menu_index = 1;
            param.Add(menu_index, "groupId (string)"); menu_index++;
            param.Add(menu_index, "filePath (string)"); menu_index++;
            level3_menus.Add("UploadGroupSharedFile", new Dictionary<int, string>(param));
            param.Clear();
        }

        internal void InitLevel2Menus_IPushManager()
        {
            Dictionary<int, string> functions_IPushManager = new Dictionary<int, string>();
            int menu_index = 1;
            functions_IPushManager.Add(menu_index, "GetNoDisturbGroups"); menu_index++;
            functions_IPushManager.Add(menu_index, "GetPushConfig"); menu_index++;
            functions_IPushManager.Add(menu_index, "GetPushConfigFromServer"); menu_index++;
            functions_IPushManager.Add(menu_index, "UpdatePushNickName"); menu_index++;
            functions_IPushManager.Add(menu_index, "UpdateHMSPushToken"); menu_index++;
            functions_IPushManager.Add(menu_index, "UpdateFCMPushToken"); menu_index++;
            functions_IPushManager.Add(menu_index, "UpdateAPNSPushToken"); menu_index++;
            functions_IPushManager.Add(menu_index, "SetNoDisturb"); menu_index++;
            functions_IPushManager.Add(menu_index, "SetPushStyle"); menu_index++;
            functions_IPushManager.Add(menu_index, "SetGroupToDisturb"); menu_index++;
            //functions_IPushManager.Add(menu_index, "ReportPushAction"); menu_index++;
            level2_menus.Add("IPushManager", functions_IPushManager);
        }

        internal void InitLevel3Menus_IPushManager()
        {
            Dictionary<int, string> param = new Dictionary<int, string>();
            int menu_index = 1;
            param.Add(menu_index, "No params"); menu_index++;
            level3_menus.Add("GetNoDisturbGroups", new Dictionary<int, string>(param));
            param.Clear();

            menu_index = 1;
            param.Add(menu_index, "No params"); menu_index++;
            level3_menus.Add("GetPushConfig", new Dictionary<int, string>(param));
            param.Clear();

            menu_index = 1;
            param.Add(menu_index, "No params"); menu_index++;
            level3_menus.Add("GetPushConfigFromServer", new Dictionary<int, string>(param));
            param.Clear();

            menu_index = 1;
            param.Add(menu_index, "nickname (string)"); menu_index++;
            level3_menus.Add("UpdatePushNickName", new Dictionary<int, string>(param));
            param.Clear();

            menu_index = 1;
            param.Add(menu_index, "token (string)"); menu_index++;
            level3_menus.Add("UpdateHMSPushToken", new Dictionary<int, string>(param));
            param.Clear();

            menu_index = 1;
            param.Add(menu_index, "token (string)"); menu_index++;
            level3_menus.Add("UpdateFCMPushToken", new Dictionary<int, string>(param));
            param.Clear();

            menu_index = 1;
            param.Add(menu_index, "token (string)"); menu_index++;
            level3_menus.Add("UpdateAPNSPushToken", new Dictionary<int, string>(param));
            param.Clear();

            menu_index = 1;
            param.Add(menu_index, "noDisturb (0:false, 1:true)"); menu_index++;
            param.Add(menu_index, "startTime (int)"); menu_index++;
            param.Add(menu_index, "endTime (int)"); menu_index++;
            level3_menus.Add("SetNoDisturb", new Dictionary<int, string>(param));
            param.Clear();

            menu_index = 1;
            param.Add(menu_index, "pushStyle (0:Simple; 1:Summary)"); menu_index++;
            level3_menus.Add("SetPushStyle", new Dictionary<int, string>(param));
            param.Clear();

            menu_index = 1;
            param.Add(menu_index, "groupId (string)"); menu_index++;
            param.Add(menu_index, "noDisturb (0:false; 1:true)"); menu_index++;
            level3_menus.Add("SetGroupToDisturb", new Dictionary<int, string>(param));
            param.Clear();

            menu_index = 1;
            param.Add(menu_index, "parameters (string)"); menu_index++;
            level3_menus.Add("ReportPushAction", new Dictionary<int, string>(param));
            param.Clear();
        }

        internal void InitLevel2Menus_IRoomManager()
        {
            Dictionary<int, string> functions_IRoomManager = new Dictionary<int, string>();
            int menu_index = 1;
            functions_IRoomManager.Add(menu_index, "AddRoomAdmin"); menu_index++;
            functions_IRoomManager.Add(menu_index, "BlockRoomMembers"); menu_index++;
            functions_IRoomManager.Add(menu_index, "ChangeRoomOwner"); menu_index++;
            functions_IRoomManager.Add(menu_index, "ChangeRoomDescription"); menu_index++;
            functions_IRoomManager.Add(menu_index, "ChangeRoomName"); menu_index++;
            functions_IRoomManager.Add(menu_index, "CreateRoom"); menu_index++;
            functions_IRoomManager.Add(menu_index, "DestroyRoom"); menu_index++;
            functions_IRoomManager.Add(menu_index, "FetchPublicRoomsFromServer"); menu_index++;
            functions_IRoomManager.Add(menu_index, "FetchRoomAnnouncement"); menu_index++;
            functions_IRoomManager.Add(menu_index, "FetchRoomBlockList"); menu_index++;
            functions_IRoomManager.Add(menu_index, "FetchRoomInfoFromServer"); menu_index++;
            functions_IRoomManager.Add(menu_index, "FetchRoomMembers"); menu_index++;
            functions_IRoomManager.Add(menu_index, "FetchRoomMuteList"); menu_index++;
            functions_IRoomManager.Add(menu_index, "JoinRoom"); menu_index++;
            functions_IRoomManager.Add(menu_index, "LeaveRoom"); menu_index++;
            functions_IRoomManager.Add(menu_index, "MuteRoomMembers"); menu_index++;
            functions_IRoomManager.Add(menu_index, "RemoveRoomAdmin"); menu_index++;
            functions_IRoomManager.Add(menu_index, "DeleteRoomMembers"); menu_index++;
            functions_IRoomManager.Add(menu_index, "UnBlockRoomMembers"); menu_index++;
            functions_IRoomManager.Add(menu_index, "UnMuteRoomMembers"); menu_index++;
            functions_IRoomManager.Add(menu_index, "UpdateRoomAnnouncement"); menu_index++;
            level2_menus.Add("IRoomManager", functions_IRoomManager);
        }

        internal void InitLevel3Menus_IRoomManager()
        {
            Dictionary<int, string> param = new Dictionary<int, string>();
            int menu_index = 1;
            param.Add(menu_index, "roomId (string)"); menu_index++;
            param.Add(menu_index, "memberId (string)"); menu_index++;
            level3_menus.Add("AddRoomAdmin", new Dictionary<int, string>(param));
            param.Clear();

            menu_index = 1;
            param.Add(menu_index, "roomId (string)"); menu_index++;
            param.Add(menu_index, "memberId1 (string)"); menu_index++;
            param.Add(menu_index, "memberId2 (string)"); menu_index++;
            level3_menus.Add("BlockRoomMembers", new Dictionary<int, string>(param));
            param.Clear();

            menu_index = 1;
            param.Add(menu_index, "roomId (string)"); menu_index++;
            param.Add(menu_index, "newOwner (string)"); menu_index++;
            level3_menus.Add("ChangeRoomOwner", new Dictionary<int, string>(param));
            param.Clear();

            menu_index = 1;
            param.Add(menu_index, "roomId (string)"); menu_index++;
            param.Add(menu_index, "newDescription (string)"); menu_index++;
            level3_menus.Add("ChangeRoomDescription", new Dictionary<int, string>(param));
            param.Clear();

            menu_index = 1;
            param.Add(menu_index, "roomId (string)"); menu_index++;
            param.Add(menu_index, "newName (string)"); menu_index++;
            level3_menus.Add("ChangeRoomName", new Dictionary<int, string>(param));
            param.Clear();

            menu_index = 1;
            param.Add(menu_index, "name (string)"); menu_index++;
            param.Add(menu_index, "descriptions (string)"); menu_index++;
            param.Add(menu_index, "welcomeMsg (string)"); menu_index++;
            param.Add(menu_index, "maxUserCount (int)"); menu_index++;
            param.Add(menu_index, "memberId1 (string)"); menu_index++;
            param.Add(menu_index, "memberId2 (string)"); menu_index++;
            level3_menus.Add("CreateRoom", new Dictionary<int, string>(param));
            param.Clear();

            menu_index = 1;
            param.Add(menu_index, "roomId (string)"); menu_index++;
            level3_menus.Add("DestroyRoom", new Dictionary<int, string>(param));
            param.Clear();

            menu_index = 1;
            param.Add(menu_index, "pageNum (int)"); menu_index++;
            param.Add(menu_index, "pageSize (int)"); menu_index++;
            level3_menus.Add("FetchPublicRoomsFromServer", new Dictionary<int, string>(param));
            param.Clear();

            menu_index = 1;
            param.Add(menu_index, "roomId (string)"); menu_index++;
            level3_menus.Add("FetchRoomAnnouncement", new Dictionary<int, string>(param));
            param.Clear();

            menu_index = 1;
            param.Add(menu_index, "roomId (string)"); menu_index++;
            param.Add(menu_index, "pageNum (int)"); menu_index++;
            param.Add(menu_index, "pageSize (int)"); menu_index++;
            level3_menus.Add("FetchRoomBlockList", new Dictionary<int, string>(param));
            param.Clear();

            menu_index = 1;
            param.Add(menu_index, "roomId (string)"); menu_index++;
            level3_menus.Add("FetchRoomInfoFromServer", new Dictionary<int, string>(param));
            param.Clear();

            menu_index = 1;
            param.Add(menu_index, "roomId (string)"); menu_index++;
            param.Add(menu_index, "cursor (string)"); menu_index++;
            param.Add(menu_index, "pageSize (int)"); menu_index++;
            level3_menus.Add("FetchRoomMembers", new Dictionary<int, string>(param));
            param.Clear();

            menu_index = 1;
            param.Add(menu_index, "roomId (string)"); menu_index++;
            param.Add(menu_index, "pageSize (int)"); menu_index++;
            param.Add(menu_index, "pageNum (int)"); menu_index++;
            level3_menus.Add("FetchRoomMuteList", new Dictionary<int, string>(param));
            param.Clear();

            menu_index = 1;
            param.Add(menu_index, "roomId (string)"); menu_index++;
            level3_menus.Add("JoinRoom", new Dictionary<int, string>(param));
            param.Clear();

            menu_index = 1;
            param.Add(menu_index, "roomId (string)"); menu_index++;
            level3_menus.Add("LeaveRoom", new Dictionary<int, string>(param));
            param.Clear();

            menu_index = 1;
            param.Add(menu_index, "roomId (string)"); menu_index++;
            param.Add(menu_index, "memberId1 (string)"); menu_index++;
            param.Add(menu_index, "memberId2 (string)"); menu_index++;
            level3_menus.Add("MuteRoomMembers", new Dictionary<int, string>(param));
            param.Clear();

            menu_index = 1;
            param.Add(menu_index, "roomId (string)"); menu_index++;
            param.Add(menu_index, "adminId (string)"); menu_index++;
            level3_menus.Add("RemoveRoomAdmin", new Dictionary<int, string>(param));
            param.Clear();

            menu_index = 1;
            param.Add(menu_index, "roomId (string)"); menu_index++;
            param.Add(menu_index, "memberId1 (string)"); menu_index++;
            param.Add(menu_index, "memberId2 (string)"); menu_index++;
            level3_menus.Add("DeleteRoomMembers", new Dictionary<int, string>(param));
            param.Clear();

            menu_index = 1;
            param.Add(menu_index, "roomId (string)"); menu_index++;
            param.Add(menu_index, "memberId1 (string)"); menu_index++;
            param.Add(menu_index, "memberId2 (string)"); menu_index++;
            level3_menus.Add("UnBlockRoomMembers", new Dictionary<int, string>(param));
            param.Clear();

            menu_index = 1;
            param.Add(menu_index, "roomId (string)"); menu_index++;
            param.Add(menu_index, "memberId1 (string)"); menu_index++;
            param.Add(menu_index, "memberId2 (string)"); menu_index++;
            level3_menus.Add("UnMuteRoomMembers", new Dictionary<int, string>(param));
            param.Clear();

            menu_index = 1;
            param.Add(menu_index, "roomId (string)"); menu_index++;
            param.Add(menu_index, "announcement (string)"); menu_index++;
            level3_menus.Add("UpdateRoomAnnouncement", new Dictionary<int, string>(param));
            param.Clear();
        }

        internal void InitLevel2Menus_IUserInfoManager()
        {
            Dictionary<int, string> functions_IUserInfoManager = new Dictionary<int, string>();
            int menu_index = 1;
            functions_IUserInfoManager.Add(menu_index, "UpdateOwnInfo"); menu_index++;
            functions_IUserInfoManager.Add(menu_index, "FetchUserInfoByUserId"); menu_index++;
            level2_menus.Add("IUserInfoManager", functions_IUserInfoManager);
        }

        internal void InitLevel3Menus_IUserInfoManager()
        {
            Dictionary<int, string> param = new Dictionary<int, string>();
            int menu_index = 1;
            param.Add(menu_index, "userId (string)"); menu_index++;
            param.Add(menu_index, "nickName (string)"); menu_index++;
            param.Add(menu_index, "avatarUrl (string)"); menu_index++;
            param.Add(menu_index, "email (string)"); menu_index++;
            param.Add(menu_index, "gender (int)"); menu_index++;
            level3_menus.Add("UpdateOwnInfo", new Dictionary<int, string>(param));
            param.Clear();

            menu_index = 1;
            param.Add(menu_index, "userId1 (string)"); menu_index++;
            param.Add(menu_index, "userId2 (string)"); menu_index++;
            level3_menus.Add("FetchUserInfoByUserId", new Dictionary<int, string>(param));
            param.Clear();
        }

        public void InitAll(string appkey)
        {
            Options options = new Options("easemob-demo#easeim");
            if (appkey.Length > 0 && appkey.Contains("#") == true)
                options.AppKey = appkey;

            options.AutoLogin = false;
            options.UsingHttpsOnly = true;
            options.DebugMode = true;
            SDKClient.Instance.InitWithOptions(options);

            ChatManagerDelegate chatManagerDelegate = new ChatManagerDelegate();
            SDKClient.Instance.ChatManager.AddChatManagerDelegate(chatManagerDelegate);

            ConnectionDelegate connectionDelegate = new ConnectionDelegate();
            SDKClient.Instance.AddConnectionDelegate(connectionDelegate);

            ContactManagerDelegate contactManagerDelegate = new ContactManagerDelegate();
            SDKClient.Instance.ContactManager.AddContactManagerDelegate(contactManagerDelegate);

            GroupManagerDelegate groupManagerDelegate = new GroupManagerDelegate();
            SDKClient.Instance.GroupManager.AddGroupManagerDelegate(groupManagerDelegate);

            MultiDeviceDelegate multiDeviceDelegate = new MultiDeviceDelegate();
            SDKClient.Instance.AddMultiDeviceDelegate(multiDeviceDelegate);

            RoomManagerDelegate roomManagerDelegate = new RoomManagerDelegate();
            SDKClient.Instance.RoomManager.AddRoomManagerDelegate(roomManagerDelegate);

            InitLevel1Menus();

            InitLevel2Menus_IClient();
            InitLevel3Menus_IClient();

            InitLevel2Menus_IChatManager();
            InitLevel3Menus_IChatManager();

            InitLevel2Menus_IContactManager();
            InitLevel3Menus_IContactManager();

            InitLevel2Menus_IConversationManager();
            InitLevel3Menus_IConversationManager();

            InitLevel2Menus_IGroupManager();
            InitLevel3Menus_IGroupManager();

            InitLevel2Menus_IPushManager();
            InitLevel3Menus_IPushManager();

            InitLevel2Menus_IRoomManager();
            InitLevel3Menus_IRoomManager();

            InitLevel2Menus_IUserInfoManager();
            InitLevel3Menus_IUserInfoManager();
        }

         /*
         * leve11 menu=======
         * Please select test manager below:
         * 
         * 1. ChatManager
         * 2. ContatctManager
         * 3. GroupManager
         * 
         * Your select: 
         */         
        public void ShowLevel1Menu()
        {
            select_context.level1_item = "";
            select_context.level2_item = "";
            if (null != select_context.level3_params)
                select_context.level3_params.Clear();
            select_context.current_level = 2;

            Console.Clear();
            Console.WriteLine("leve11 menu=======");
            Console.WriteLine("Please select test manager below:");
            Console.WriteLine("");
            Console.WriteLine("");
            foreach(var it in level1_menus)
            {
                string str = it.Key + ". " + it.Value;
                Console.WriteLine(str);
            }
            Console.WriteLine("");
            Console.WriteLine("");
            Console.Write("Your Select:");

            if (!ReadSelectedIndex())
            {
                // 读取错误，返回当前菜单
                Console.WriteLine("Press any key...");
                Console.ReadKey();
                ShowLevel1Menu();
            }
            else
            {
                // 读取成功，进入下一级菜单
                ShowLevel2Menu(select_context.level1_item);
            }                
        }

         /* leve12 menu=======
         * 
         * level2 menu:
         * Please select test function below:
         * Current Manager: ChatManager
         *
         * 1. SendMessage                             5. GetUnreadMessageCount              9. FuncA
         * 2. FuncB                                   6. ImportMessages
         * 3. DownloadAttachment                      7. LoadAllConversations
         * 4. FetchHistoryMessagesFromServer          8. MarkAllConversationsAsRead
         *
         * Your select: I
         */
        public void ShowLevel2Menu(string level1_selected)
        {
            select_context.level1_item = level1_selected;
            select_context.level2_item = "";
            if (null != select_context.level3_params)
                select_context.level3_params.Clear();
            select_context.current_level = 2;

            Console.Clear();
            Console.WriteLine("leve12 menu=======");
            Console.WriteLine("Please select test function below:");
            Console.WriteLine($"Current Manager: {select_context.level1_item}");
            Console.WriteLine("");
            Console.WriteLine("");

            Dictionary<int, string> functions = level2_menus[level1_selected];
            if(functions.Count == 0)
            {
                Console.WriteLine($"No functions under {level1_selected}");
            }

            int menu_width = 3; // 菜单列数
            int menu_height = functions.Count / menu_width; // 菜单行数
            if (functions.Count != menu_height * menu_width)
                menu_height++;

            int menu_count = 0;
            int height_it = 0; // 行高递增变量

            while (height_it < menu_height) // 生成行
            {
                height_it++;  // 行高加1
                int index = height_it; // 每一层的第一个index为行高

                // 生成列
                for (int i=0; i< menu_width; i++)
                {
                    if(functions.ContainsKey(index))
                    {
                        string str = index.ToString() + ". " + functions[index];
                        if (i < menu_width - 1)
                            Console.Write("{0,-50}  ", str);
                        else
                            Console.WriteLine("{0,-50}   ", str);
                        menu_count++;
                    }
                    else
                    {
                        Console.WriteLine("");
                    }
                    index = index + menu_height;
                }
            }

            if(menu_count != functions.Count)
            {
                Console.WriteLine($"Error: missing some menus! all:{functions.Count}, generated:{menu_count}");
            }

            Console.WriteLine("");
            Console.WriteLine("");
            Console.Write("Your Select:");

            if (!ReadSelectedIndex())
            {
                // 读取失败，返回当前菜单
                Console.WriteLine("Press any key...");
                Console.ReadKey();
                ShowLevel2Menu(select_context.level1_item);
            }
            else
            {
                // 读取成功，进入下一级菜单或者返回上一级菜单(-1)
                if (select_context.level1_item.CompareTo("") == 0 && select_context.current_level == 1)
                    ShowLevel1Menu();
                else if (select_context.level2_item.CompareTo("") == 0 && select_context.current_level == 2)
                    ShowLevel2Menu(select_context.level1_item);
                else
                    ShowLevel3Menu(select_context.level2_item);
            }
        }

        /*
         * leve13 menu=======
         *          
         * Please fill parameters for function: FetchHistoryMessagesFromServer
         * Current Manager: ChatManager
         * 
         * 1. conversationId
         * 2. conversation_Type (0:Chat; 1:Group; 2:Room)
         * 3. startMessageId
         * 4. count
         * 
         * Your Input:
         * yqtest1;0;;250
         * 
         * Running case, result is:
         */

        public void ShowLevel3Menu(string level2_selected)
        {
            select_context.level2_item = level2_selected;

            Console.Clear();
            Console.WriteLine("leve13 menu=======");
            Console.WriteLine($"Please fill parameters for function: {level2_selected}");
            Console.WriteLine($"Current Manager: {select_context.level1_item}");
            Console.WriteLine("");
            Console.WriteLine("");

            Dictionary<int, string> paramters = level3_menus[level2_selected];
            if (paramters.Count == 0)
            {
                Console.WriteLine($"No functions for function: {level2_selected}");
            }

            foreach(var it in paramters)
            {
                string str = it.Key + ". " + it.Value;
                Console.WriteLine(str);
            }

            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("Fill parameter values:");

            if (!ReadParamValues())
            {
                // 读取失败，返回当前菜单
                Console.WriteLine("Press any key...");
                Console.ReadKey();
                ShowLevel3Menu(select_context.level2_item);
            }
            else
            {
                // 运行case
                CallFunc();

                //等待case运行完成2s
                Thread.Sleep(2000);
                Console.WriteLine("Running done.");

                // 进行下一次操作
                string key = "";
                while (true) {
                    Console.WriteLine("Press: b-current menu; m-main menu; u-up level menu");
                    key = Console.ReadLine();
                    if (key.CompareTo("b") != 0 && key.CompareTo("m") != 0 && key.CompareTo("u") != 0)
                    {
                        Console.WriteLine("Wrong input.");
                    }
                    else
                        break;
                }

                if (key.CompareTo("b") == 0)
                {
                    select_context.level3_params.Clear();
                    select_context.current_level = 3;
                    ShowLevel3Menu(select_context.level2_item);
                    return;
                }

                if (key.CompareTo("m") == 0)
                {
                    select_context.level1_item = "";
                    select_context.level2_item = "";
                    select_context.level3_params.Clear();
                    select_context.current_level = -1;
                    ShowLevel1Menu();
                    return;
                }

                if (key.CompareTo("u") == 0)
                {
                    select_context.level2_item = "";
                    select_context.level3_params.Clear();
                    select_context.current_level = 2;
                    ShowLevel2Menu(select_context.level1_item);
                    return;
                }
            }
        }

        public bool ReadSelectedIndex()
        {
            string manager_index = Console.ReadLine();
            int index = -1;

            if(manager_index.Length == 0)
            {
                index = 0;
            }
            else
            {
                int iResult = 0;
                if (int.TryParse(manager_index, out iResult) == true)
                {
                    index = int.Parse(manager_index);
                }
            }

            if (null == select_context.level1_item || 0 == select_context.level1_item.Length)
            {
                if (index <= 0)
                {
                    Console.WriteLine($"Error input: {manager_index}");
                    return false;
                }

                if (index > level1_menus.Count)
                {
                    Console.WriteLine($"Error input: {manager_index}");
                    return false;
                }
                select_context.level1_item = level1_menus[index];
                select_context.current_level = 1;
            }
            else
            {
                // 用于返回上一级路径
                if (index < 0)
                {
                    select_context.level1_item = "";
                    select_context.current_level = 1;
                    return true;
                }
                else if (0 == index)
                {
                    select_context.level2_item = "";
                    select_context.current_level = 2;
                    return true;
                }

                if (index > level2_menus[select_context.level1_item].Count)
                {
                    Console.WriteLine($"Error input: {manager_index}");
                    return false;
                }
                if (select_context.level1_item.CompareTo("") == 0)
                {
                    Console.WriteLine("Fatal error for select context!");
                    Console.ReadKey();                    
                    Environment.Exit(-1);
                }
                select_context.level2_item = level2_menus[select_context.level1_item][index];
                select_context.current_level = 2;
            }
            return true;
        }

        public bool ReadParamValues()
        {
            string values = Console.ReadLine();

            if(select_context.level1_item.CompareTo("") == 0 || select_context.level2_item.CompareTo("") == 0)
            {
                Console.WriteLine("Fatal error for select context!");
                Console.ReadKey();
                Environment.Exit(-1);
            }

            int param_num = level3_menus[select_context.level2_item].Count;
            if (1 == param_num && level3_menus[select_context.level2_item][1].CompareTo("No params") == 0)
                return true;

            if (values.Length == 0)
            {
                Console.WriteLine($"Error input: parame values expected.");
                return false;
            }

            List<string> list = Utils.SplitString(values);
            if(list.Count != param_num)
            {
                Console.WriteLine($"Error input: params not matched with input, param num:{param_num}, input num:{list.Count}.");
                return false;
            }

            select_context.level3_params = list;
            select_context.current_level = 3;
            return true;
        }

        public void CallFunc_IClient_CreateAccount(string username_input="", string password_input="")
        {
            string username = "";
            if (username_input.Length > 0) 
                username = username_input;
            else
                username = GetParamValueFromContext(0);

            string password = "";
            if (password_input.Length > 0)
                password = password_input;
            else
                password = GetParamValueFromContext(1);

            SDKClient.Instance.CreateAccount(username, password,
                handle: new CallBack(

                onSuccess: () =>
                {
                    Console.WriteLine("CreateAccount succeed");
                },

                onError: (code, desc) =>
                {
                    Console.WriteLine($"CreateAccount failed, code:{code}, desc:{desc}");
                }
            ));
        }

        public void CallFunc_IClient_Login(string username_input="", string password_input="", bool istoken_input=false)
        {
            string username = "";
            if (username_input.Length > 0)
                username = username_input;
            else
                username = GetParamValueFromContext(0);

            string password = "";
            if (password_input.Length > 0)
                password = password_input;
            else
                password = GetParamValueFromContext(1);

            bool istoken = false;
            if (select_context.level3_params[1].CompareTo("true") == 0)
                istoken = true;
            if (istoken_input)
                istoken = true;

            SDKClient.Instance.Login(username, password, istoken,
            handle: new CallBack(

                onSuccess: () =>
                {
                    Console.WriteLine("Login succeed");
                },

                onError: (code, desc) =>
                {
                    if (code == 200)
                    {
                        Console.WriteLine($"Already login");
                    }
                    else
                    {
                        Console.WriteLine($"Login failed, code:{code}, desc:{desc}");
                    }
                }
                )
            );
        }

        public void CallFunc_IClient_Logout()
        {
            SDKClient.Instance.Logout(false,
                handle: new CallBack(
                onSuccess: () =>
                {
                    Console.WriteLine("Logout succeed");
                },

                onError: (code, desc) =>
                {
                    Console.WriteLine($"Login failed, code:{code}, desc:{desc}");
                }
                )
            );
        }

        public void CallFunc_IClient_CurrentUsername()
        {
            string user = SDKClient.Instance.CurrentUsername;
            Console.WriteLine($"Currenet user is: {user}");
        }

        public void CallFunc_IClient_IsConnected()
        {
            bool isConnected = SDKClient.Instance.IsConnected;
            string isConnectedStr = isConnected ? "connected" : "diconnected";
            Console.WriteLine($"Connected? {isConnectedStr}");
        }

        public void CallFunc_IClient_IsLoggedIn()
        {
            bool isLoggedIn = SDKClient.Instance.IsLoggedIn;
            string isLoggedInStr = isLoggedIn ? "Logined" : "Logouted";
            Console.WriteLine($"Login in status: {isLoggedInStr}");
        }

        public void CallFunc_IClient_AccessToken()
        {
            string accessToken = SDKClient.Instance.AccessToken;
            Console.WriteLine($"AccessToken: {accessToken}");
        }

        public void CallFunc_IClient_LoginWithAgoraToken(string username_input = "", string token_input = "")
        {
            string username = "";
            string token = "";

            if (username_input.Length > 0)
                username = username_input;
            else
                username = GetParamValueFromContext(0);

            if (token_input.Length > 0)
                token = token_input;
            else
                token = GetParamValueFromContext(1);

            SDKClient.Instance.LoginWithAgoraToken(username, token, 
            handle: new CallBack(

                onSuccess: () =>
                {
                    Console.WriteLine("LoginWithAgoraToken succeed");
                },

                onError: (code, desc) =>
                {
                    if (code == 200)
                    {
                        Console.WriteLine($"Already login");
                    }
                    else
                    {
                        Console.WriteLine($"Login failed, code:{code}, desc:{desc}");
                    }
                }
                )
            );
        }

        public void CallFunc_IClient_RenewAgoraToken(string token_input = "")
        {
            string token = "";
            if (token_input.Length > 0)
                token = token_input;
            else
                token = GetParamValueFromContext(0);

            SDKClient.Instance.RenewAgoraToken(token);
            Console.WriteLine($"RenewAgoraToken complete.");
        }

        public void CallFunc_IClient()
        {
            if (select_context.level2_item.CompareTo("CreateAccount") == 0)
            {
                CallFunc_IClient_CreateAccount();
                return;
            }

            if (select_context.level2_item.CompareTo("Login") == 0)
            {
                CallFunc_IClient_Login();
                return;
            }

            if (select_context.level2_item.CompareTo("Logout") == 0)
            {
                CallFunc_IClient_Logout();
                return;
            }

            if (select_context.level2_item.CompareTo("CurrentUsername") == 0)
            {
                CallFunc_IClient_CurrentUsername();
                return;
            }

            if (select_context.level2_item.CompareTo("IsConnected") == 0)
            {
                CallFunc_IClient_IsConnected();
                return;
            }

            if (select_context.level2_item.CompareTo("IsLoggedIn") == 0)
            {
                CallFunc_IClient_IsLoggedIn();
                return;
            }

            if (select_context.level2_item.CompareTo("AccessToken") == 0)
            {
                CallFunc_IClient_AccessToken();
                return;
            }

            if (select_context.level2_item.CompareTo("LoginWithAgoraToken") == 0)
            {
                CallFunc_IClient_LoginWithAgoraToken();
                return;
            }

            if (select_context.level2_item.CompareTo("RenewAgoraToken") == 0)
            {
                CallFunc_IClient_RenewAgoraToken();
                return;
            }
        }

        public void CallFunc_IChatManager_DeleteConversation(string conversationId_input="", bool deleteMessages_input=true)
        {
            string conversationId = "";

            if (conversationId_input.Length > 0)
                conversationId = conversationId_input;
            else
                conversationId = GetParamValueFromContext(0);

            bool deleteMessages = true;
            if (GetParamValueFromContext(1).CompareTo("false") == 0)
                deleteMessages = false;
            if (deleteMessages_input)
                deleteMessages = false;

            bool ret = SDKClient.Instance.ChatManager.DeleteConversation(conversationId, deleteMessages);
            string ret_str = ret ? "true" : "false";
            Console.WriteLine($"DeleteConversation return {ret_str}.");
        }

        public void CallFunc_IChatManager_DownloadAttachment(string messageId_input ="")
        {
            string messageId = "";

            if (messageId_input.Length > 0)
                messageId = messageId_input;
            else
                messageId = GetParamValueFromContext(0);

            SDKClient.Instance.ChatManager.DownloadAttachment(messageId, new CallBack(
                onSuccess: () => {
                    Console.WriteLine("DownloadAttachment complete");
                },
                onProgress: (progress) => {
                    Console.WriteLine($"DownloadAttachment progress: {progress.ToString()}");
                },
                onError: (code, desc) => {
                    Console.WriteLine($"DownloadAttachment failed, code:{code}, desc:{desc}");
                }
            ));
        }

        public void CallFunc_IChatManager_DownloadThumbnail(string messageId_input = "")
        {
            string messageId = "";

            if (messageId_input.Length > 0)
                messageId = messageId_input;
            else
                messageId = GetParamValueFromContext(0);

            SDKClient.Instance.ChatManager.DownloadThumbnail(messageId, new CallBack(
                onSuccess: () => {
                    Console.WriteLine("DownloadThumbnail complete");
                },
                onProgress: (progress) => {
                    Console.WriteLine($"DownloadThumbnail progress: {progress.ToString()}");
                },
                onError: (code, desc) => {
                    Console.WriteLine($"DownloadThumbnail failed, code:{code}, desc:{desc}");
                }
            ));
        }

        public void CallFunc_IChatManager_FetchHistoryMessagesFromServer(string conversationId_input="", int type_input = -1, string startMessageId_input="", int count_input = -1)
        {
            string conversationId = "";
            ConversationType type = ConversationType.Chat;
            string startMessageId = "";
            int count = 20;

            if (conversationId_input.Length > 0)
                conversationId = conversationId_input;
            else
                conversationId = GetParamValueFromContext(0);

            //优先已外部设置为主
            if (type_input >= 0 && type_input <= 2)
                type = (ConversationType)type_input;
            else
            {
                int i = GetIntFromString(GetParamValueFromContext(1));
                if (i >= 0 && i <= 2)
                    type = (ConversationType)i;
            }

            if (startMessageId_input.Length > 0)
                startMessageId = startMessageId_input;
            else
                startMessageId = GetParamValueFromContext(2);

            if (-1 != count_input)
                count = count_input;
            else
            {
                int i = GetIntFromString(GetParamValueFromContext(3));
                if (i > 0)
                    count = i;
            }

            SDKClient.Instance.ChatManager.FetchHistoryMessagesFromServer(conversationId, type, startMessageId, count, new ValueCallBack<CursorResult<Message>>(
                onSuccess: (result) =>
                {
                    if (0 == result.Data.Count)
                    {
                        Console.WriteLine("No history messages.");
                        return;
                    }
                    Console.WriteLine($"FetchHistoryMessagesFromServer: found {result.Data.Count} messages");
                    foreach (var msg in result.Data)
                    {
                        //Console.WriteLine($"message id: {msg.MsgId}");

                        Console.WriteLine($"===========================");
                        Console.WriteLine($"message id: {msg.MsgId}");
                        Console.WriteLine($"cov id: {msg.ConversationId}");
                        Console.WriteLine($"From: {msg.From}");
                        Console.WriteLine($"To: {msg.To}");
                        Console.WriteLine($"RecallBy: {msg.RecallBy}");
                        Console.WriteLine($"message type: {msg.MessageType}");
                        Console.WriteLine($"diection: {msg.Direction}");
                        Console.WriteLine($"status: {msg.Status}");
                        Console.WriteLine($"localtime: {msg.LocalTime}");
                        Console.WriteLine($"servertime: {msg.ServerTime}");
                        Console.WriteLine($"HasDeliverAck: {msg.HasDeliverAck}");
                        Console.WriteLine($"HasReadAck: {msg.HasReadAck}");
                        foreach (var it1 in msg.Attributes)
                        {
                            Console.WriteLine($"attribute item: key:{it1.Key}; value:{it1.Value}");
                        }
                        if (msg.Body.Type == MessageBodyType.TXT)
                        {
                            TextBody tb = (TextBody)msg.Body;
                            Console.WriteLine($"message text content: {tb.Text}");
                        }
                        Console.WriteLine($"===========================");
                    }
                },
                onError: (code, desc) =>
                {
                    Console.WriteLine($"FetchHistoryMessagesFromServer failed, code:{code}, desc:{desc}");
                }
            ));
        }

        public void CallFunc_IChatManager_GetConversation(string conversationId_input="", int type_input=-1, bool createIfNeed_input=true)
        {
            string conversationId = "";
            ConversationType type = ConversationType.Chat;
            bool createIfNeed = true;

            if (conversationId_input.Length > 0)
                conversationId = conversationId_input;
            else
                conversationId = GetParamValueFromContext(0);

            if (type_input >= 0 && type_input <= 2)
                type = (ConversationType)type_input;
            else
            {
                int i = GetIntFromString(GetParamValueFromContext(1));
                if (i >= 0 && i <= 2)
                    type = (ConversationType)i;
            }

            if (GetParamValueFromContext(2).CompareTo("false") == 0)
                createIfNeed = false;
            if (false == createIfNeed_input)
                createIfNeed = false;

            Conversation ret = SDKClient.Instance.ChatManager.GetConversation(conversationId, type, createIfNeed);
            if (null != ret && null != ret.Id)
            Console.WriteLine($"GetConversation complete, conversationid: {ret.Id}.");
        }

        public void CallFunc_IChatManager_GetConversationsFromServer(string conversationId_input = "", int type_input = -1, bool createIfNeed_input = true)
        {
            SDKClient.Instance.ChatManager.GetConversationsFromServer(new ValueCallBack<List<Conversation>>(
             onSuccess: (list) =>
             {
                 Console.WriteLine($"GetConversationsFromServer found total: {list.Count}");
                 foreach (var conv in list)
                 {
                     Console.WriteLine($"conv id: {conv.Id}");
                 }
             },
             onError: (code, desc) =>
             {
                 Console.WriteLine($"GetConversationsFromServer failed, code:{code}, desc:{desc}");
             }
            ));
        }

        public void CallFunc_IChatManager_GetUnreadMessageCount()
        {
            int ret = SDKClient.Instance.ChatManager.GetUnreadMessageCount();
            Console.WriteLine($"GetUnreadMessageCount unread msg count: {ret}.");
        }

        public void CallFunc_IChatManager_ImportMessages()
        {
            List<Message> messages = new List<Message>();
            Message msg = Message.CreateTextSendMessage("to1", "hello");
            Message msg1 = Message.CreateTextSendMessage("to2", "world");

            messages.Add(msg);
            messages.Add(msg);

            bool ret = SDKClient.Instance.ChatManager.ImportMessages(messages);
            if(ret)
                Console.WriteLine($"ImportMessages {messages.Count} messages completed.");
            else
                Console.WriteLine($"ImportMessages {messages.Count} messages failed.");
        }

        public void CallFunc_IChatManager_LoadAllConversations()
        {
            List<Conversation> list = SDKClient.Instance.ChatManager.LoadAllConversations();
            Console.WriteLine($"LoadAllConversations total conv: {list.Count}");
            foreach (var conv in list)
            {
                Console.WriteLine($"conv id: {conv.Id}");
            }
        }

        public void CallFunc_IChatManager_LoadMessage(string messageId_input="")
        {
            string messageId = "";

            if (messageId_input.Length > 0)
                messageId = messageId_input;
            else
                messageId = GetParamValueFromContext(0);

            Message msg = SDKClient.Instance.ChatManager.LoadMessage(messageId);
            if (msg != null)
            {
                Console.WriteLine($"LoadMessage completed, msg status:{msg.Status}.");
            }
            else
            {
                Console.WriteLine($"LoadMessage failed.");
            }
        }

        public void CallFunc_IChatManager_MarkAllConversationsAsRead()
        {

            bool ret = SDKClient.Instance.ChatManager.MarkAllConversationsAsRead();
            if (ret)
            {
                int unreadCount = SDKClient.Instance.ChatManager.GetUnreadMessageCount();
                Console.WriteLine($"MarkAllConversationsAsRead completed, current unread message:{unreadCount}.");
            }
            else
            {
                Console.WriteLine($"MarkAllConversationsAsRead failed.");
            }
        }

        public void CallFunc_IChatManager_RecallMessage(string messageId_input="")
        {
            string messageId = "";

            if (messageId_input.Length > 0)
                messageId = messageId_input;
            else
                messageId = GetParamValueFromContext(0);

            SDKClient.Instance.ChatManager.RecallMessage(messageId, new CallBack(
                onSuccess: () => {
                    Console.WriteLine($"RecallMessage completed.");
                },
                onProgress: (progress) => {
                    Console.WriteLine($"RecallMessage progress:{progress.ToString()}");
                },
                onError: (code, desc) =>
                {
                    Console.WriteLine($"RecallMessage failed, code:{code}, desc:{desc}");
                }
            ));
        }

        public void CallFunc_IChatManager_ResendMessage(string messageId_input="")
        {
            string messageId = "";

            if (messageId_input.Length > 0)
                messageId = messageId_input;
            else
                messageId = GetParamValueFromContext(0);

            SDKClient.Instance.ChatManager.ResendMessage(messageId, new CallBack(
                onSuccess: () => {
                    Console.WriteLine($"ResendMessage completed.");
                },
                onProgress: (progress) => {
                    Console.WriteLine($"ResendMessage progress:{progress.ToString()}");
                },
                onError: (code, desc) =>
                {
                    Console.WriteLine($"ResendMessage failed, code:{code}, desc:{desc}");
                }
            ));
        }

        public void CallFunc_IChatManager_SearchMsgFromDB(string _keywords = "", long _timestamp = -1, int _maxCount = -1, string _from="", int _direction = -1)
        {
            string keywords = "";
            long timestamp = -1;
            int maxCount = -1;
            string from = "";
            MessageSearchDirection direction = MessageSearchDirection.UP;

            if (_keywords.Length > 0)
                keywords = _keywords;
            else
                keywords = GetParamValueFromContext(0);

            if (-1 != _timestamp)
                timestamp = _timestamp;
            else
            {
                timestamp = GetLongFromString(GetParamValueFromContext(1));
            }

            if(-1 != _maxCount)
                maxCount = _maxCount;
            else
            {
                maxCount = GetIntFromString(GetParamValueFromContext(2));
            }

            if (_from.Length > 0)
                from = _from;
            else
                from = GetParamValueFromContext(3);

            if (0 == _direction || 1 == _direction)
                direction = (MessageSearchDirection)_direction;
            else
            {
                int i = GetIntFromString(GetParamValueFromContext(3));
                if (0 == i || 1 == i)
                    direction = (MessageSearchDirection)i;
            }

            List<Message> list = SDKClient.Instance.ChatManager.SearchMsgFromDB(keywords, timestamp, maxCount, from, direction);
            if (list != null)
            {
                Console.WriteLine($"SearchMsgFromDB completed, found message {list.Count}.");
                foreach(var it in list)
                {
                    Console.WriteLine($"message id: {it.MsgId}");
                }
            }
            else
            {
                Console.WriteLine($"SearchMsgFromDB completed, not found messages.");
            }
        }

        public void CallFunc_IChatManager_SendConversationReadAck(string _conversationId="")
        {
            string conversationId = "";

            if (_conversationId.Length > 0)
                conversationId = _conversationId;
            else
                conversationId = GetParamValueFromContext(0);

            SDKClient.Instance.ChatManager.SendConversationReadAck(conversationId, new CallBack(
                onSuccess: () =>
                {
                    Console.WriteLine($"SendConversationReadAck success.");
                },
                onError: (code, desc) =>
                {
                    Console.WriteLine($"SendConversationReadAck failed, code:{code}, desc:{desc}");
                }
            ));
        }

        public void CallFunc_IChatManager_SendTxtMessage(string _to="", string _text="")
        {
            string to = "";
            string text = "";

            if (_to.Length > 0)
                to = _to;
            else
                to = GetParamValueFromContext(0);

            if (_text.Length > 0)
                text = _text;
            else
                text = GetParamValueFromContext(1);

            Message msg = Message.CreateTextSendMessage(to, text);

            SDKClient.Instance.ChatManager.SendMessage(ref msg, new CallBack(
                onSuccess: () => {
                    Console.WriteLine($"SendTxtMessage success. msgid:{msg.MsgId}");
                },
                onProgress: (progress) => {
                    Console.WriteLine($"SendTxtMessage progress :{progress.ToString()}");
                },
                onError: (code, desc) => {
                    Console.WriteLine($"SendTxtMessage failed, code:{code}, desc:{desc}");
                }
            ));
        }

        public void CallFunc_IChatManager_SendImageMessage(string _to="", string _path="")
        {
            string to = "";
            string path = "";

            if (_to.Length > 0)
                to = _to;
            else
                to = GetParamValueFromContext(0);

            if (_path.Length > 0)
                path = _path;
            else
                path = GetParamValueFromContext(1);

            Message msg = Message.CreateImageSendMessage(to, path);

            SDKClient.Instance.ChatManager.SendMessage(ref msg, new CallBack(
                onSuccess: () => {
                    Console.WriteLine($"SendImageMessage success. msgid:{msg.MsgId}");
                },
                onProgress: (progress) => {
                    Console.WriteLine($"SendImageMessage progress :{progress.ToString()}");
                },
                onError: (code, desc) => {
                    Console.WriteLine($"SendImageMessage failed, code:{code}, desc:{desc}");
                }
            ));
        }

        public void CallFunc_IChatManager_SendFileMessage(string _to="", string _path="", int fz=0)
        {
            string to = "";
            string path = "";
            int filesize = 0;

            if (_to.Length > 0)
                to = _to;
            else
                to = GetParamValueFromContext(0);

            if (_path.Length > 0)
                path = _path;
            else
                path = GetParamValueFromContext(1);

            if (fz > 0)
                filesize = fz;
            else
                filesize = GetIntFromString(GetParamValueFromContext(2));

            Message msg = Message.CreateFileSendMessage(to, path, "", filesize);

            SDKClient.Instance.ChatManager.SendMessage(ref msg, new CallBack(
                onSuccess: () => {
                    Console.WriteLine($"SendFileMessage success. msgid:{msg.MsgId}");
                },
                onProgress: (progress) => {
                    Console.WriteLine($"SendFileMessage progress :{progress.ToString()}");
                },
                onError: (code, desc) => {
                    Console.WriteLine($"SendFileMessage failed, code:{code}, desc:{desc}");
                }
            ));
        }

        public void CallFunc_IChatManager_SendVideoMessage(string _to="", string _path="")
        {
            string to = "";
            string path = "";

            if (_to.Length > 0)
                to = _to;
            else
                to = GetParamValueFromContext(0);

            if (_path.Length > 0)
                path = _path;
            else
                path = GetParamValueFromContext(1);

            Message msg = Message.CreateVideoSendMessage(to, path);

            SDKClient.Instance.ChatManager.SendMessage(ref msg, new CallBack(
                onSuccess: () => {
                    Console.WriteLine($"SendVideoMessage success. msgid:{msg.MsgId}");
                },
                onProgress: (progress) => {
                    Console.WriteLine($"SendVideoMessage progress :{progress.ToString()}");
                },
                onError: (code, desc) => {
                    Console.WriteLine($"SendVideoMessage failed, code:{code}, desc:{desc}");
                }
            ));
        }

        public void CallFunc_IChatManager_SendVoiceMessage(string _to="", string _path="")
        {
            string to = "";
            string path = "";

            if (_to.Length > 0)
                to = _to;
            else
                to = GetParamValueFromContext(0);

            if (_path.Length > 0)
                path = _path;
            else
                path = GetParamValueFromContext(1);

            Message msg = Message.CreateVoiceSendMessage(to, path);

            SDKClient.Instance.ChatManager.SendMessage(ref msg, new CallBack(
                onSuccess: () => {
                    Console.WriteLine($"SendVoiceMessage success. msgid:{msg.MsgId}");
                },
                onProgress: (progress) => {
                    Console.WriteLine($"SendVoiceMessage progress :{progress.ToString()}");
                },
                onError: (code, desc) => {
                    Console.WriteLine($"SendVoiceMessage failed, code:{code}, desc:{desc}");
                }
            ));
        }

        public void CallFunc_IChatManager_SendCmdMessage(string _to="", string _cmd="")
        {
            string to = "";
            string cmd = "";

            if (_to.Length > 0)
                to = _to;
            else
                to = GetParamValueFromContext(0);

            if (_cmd.Length > 0)
                cmd = _cmd;
            else
                cmd = GetParamValueFromContext(1);

            Message msg = Message.CreateCmdSendMessage(to, cmd);

            SDKClient.Instance.ChatManager.SendMessage(ref msg, new CallBack(
                onSuccess: () => {
                    Console.WriteLine($"SendCmdMessage success. msgid:{msg.MsgId}");
                },
                onProgress: (progress) => {
                    Console.WriteLine($"SendCmdMessage progress :{progress.ToString()}");
                },
                onError: (code, desc) => {
                    Console.WriteLine($"SendCmdMessage failed, code:{code}, desc:{desc}");
                }
            ));
        }

        public void CallFunc_IChatManager_SendCustomMessage(string _to="", string _custom="")
        {
            string to = "";
            string custom = "";

            if (_to.Length > 0)
                to = _to;
            else
                to = GetParamValueFromContext(0);

            if (_custom.Length > 0)
                custom = _custom;
            else
                custom = GetParamValueFromContext(1);

            Message msg = Message.CreateCustomSendMessage(to, custom);

            SDKClient.Instance.ChatManager.SendMessage(ref msg, new CallBack(
                onSuccess: () => {
                    Console.WriteLine($"SendCustomMessage success. msgid:{msg.MsgId}");
                },
                onProgress: (progress) => {
                    Console.WriteLine($"SendCustomMessage progress :{progress.ToString()}");
                },
                onError: (code, desc) => {
                    Console.WriteLine($"SendCustomMessage failed, code:{code}, desc:{desc}");
                }
            ));
        }

        public void CallFunc_IChatManager_SendLocationMessage(string _to="", string _addr="", string _building="")
        {
            string to = "";
            string addr = "";
            string building = "";
            double la = 123.1234;
            double lg = 456.4567;

            if (_to.Length > 0)
                to = _to;
            else
                to = GetParamValueFromContext(0);

            if (_addr.Length > 0)
                addr = _addr;
            else
                addr = GetParamValueFromContext(1);

            if (_building.Length > 0)
                building = _building;
            else
                building = GetParamValueFromContext(2);

            Message msg = Message.CreateLocationSendMessage(to, la,lg, addr, building);

            SDKClient.Instance.ChatManager.SendMessage(ref msg, new CallBack(
                onSuccess: () => {
                    Console.WriteLine($"SendLocationMessage success. msgid:{msg.MsgId}");
                },
                onProgress: (progress) => {
                    Console.WriteLine($"SendLocationMessage progress :{progress.ToString()}");
                },
                onError: (code, desc) => {
                    Console.WriteLine($"SendLocationMessage failed, code:{code}, desc:{desc}");
                }
            ));
        }

        public void CallFunc_IChatManager_SendMessageReadAck(string _messageId="")
        {
            string messageId = "";

            if (_messageId.Length > 0)
                messageId = _messageId;
            else
                messageId = GetParamValueFromContext(0);

            SDKClient.Instance.ChatManager.SendMessageReadAck(messageId, new CallBack(
                onSuccess: () =>
                {
                    Console.WriteLine($"SendMessageReadAck success.");
                },
                onError: (code, desc) =>
                {
                    Console.WriteLine($"SendMessageReadAck failed, code:{code}, desc:{desc}");
                }
            ));
        }

        public void CallFunc_IChatManager_ChatManagerUpdateMessage(string _messageId="", int _status=-1)
        {
            string messageId = "";
            MessageStatus status = MessageStatus.CREATE;

            if (_messageId.Length > 0)
                messageId = _messageId;
            else
                messageId = GetParamValueFromContext(0);

            Message msg = SDKClient.Instance.ChatManager.LoadMessage(messageId);
            if (null == msg)
            {
                Console.WriteLine($"ChatManagerUpdateMessage cannot find the message with id {messageId}.");
                return;
            }

            if (_status >= 0 && _status <= 3)
                status = (MessageStatus)_status;
            else
            {
                int i = GetIntFromString(GetParamValueFromContext(1));
                if (i >= 0 && i <= 3)
                    status = (MessageStatus)i;
            }

            msg.Status = status;

            bool ret = SDKClient.Instance.ChatManager.UpdateMessage(msg);
            if(ret)
                Console.WriteLine($"ChatManagerUpdateMessage success.");
            else
                Console.WriteLine($"ChatManagerUpdateMessage failed.");
        }

        public void CallFunc_IChatManager_RemoveMessagesBeforeTimestamp(long _ts = -1)
        {
            long ts = -1;

            if (-1 != _ts)
                ts = _ts;
            else
                ts = GetLongFromString(GetParamValueFromContext(0));

            SDKClient.Instance.ChatManager.RemoveMessagesBeforeTimestamp(ts, new CallBack(
                onSuccess: () =>
                {
                    Console.WriteLine($"RemoveMessagesBeforeTimestamp success.");
                },
                onError: (code, desc) =>
                {
                    Console.WriteLine($"RemoveMessagesBeforeTimestamp failed, code:{code}, desc:{desc}");
                }
            ));
        }

        public void CallFunc_IChatManager_DeleteConversationFromServer(string _cid="", int _type=-1, int _isdel=-1)
        {
            string cid = "";
            ConversationType type = ConversationType.Chat;
            bool isdel = true;

            if (_cid.Length > 0)
                cid = _cid;
            else
                cid = GetParamValueFromContext(0);

            if (_type >= 0 && _type <= 2)
                type = (ConversationType)_type;
            else
            {
                int i = GetIntFromString(GetParamValueFromContext(1));
                if (i >= 0 && i <= 2)
                    type = (ConversationType)i;
            }

            if(0 == _isdel || 1 == _isdel)
            {
                isdel = (1 == _isdel) ? true : false;
            }
            else
            {
                int i = GetIntFromString(GetParamValueFromContext(2));
                if (0 == i || 1 == i)
                {
                    isdel = (1 == i) ? true : false;
                }
            }

            SDKClient.Instance.ChatManager.DeleteConversationFromServer(cid, type, isdel, new CallBack(
                onSuccess: () =>
                {
                    Console.WriteLine($"DeleteConversationFromServer success.");
                },
                onError: (code, desc) =>
                {
                    Console.WriteLine($"DeleteConversationFromServer failed, code:{code}, desc:{desc}");
                }
            ));
        }


        public void CallFunc_IChatManager()
        {
            if (select_context.level2_item.CompareTo("DeleteConversation") == 0)
            {
                CallFunc_IChatManager_DeleteConversation();
                return;
            }

            if (select_context.level2_item.CompareTo("DownloadAttachment") == 0)
            {
                CallFunc_IChatManager_DownloadAttachment();
                return;
            }

            if (select_context.level2_item.CompareTo("DownloadThumbnail") == 0)
            {
                CallFunc_IChatManager_DownloadThumbnail();
                return;
            }

            if (select_context.level2_item.CompareTo("FetchHistoryMessagesFromServer") == 0)
            {
                CallFunc_IChatManager_FetchHistoryMessagesFromServer();
                return;
            }

            if (select_context.level2_item.CompareTo("GetConversation") == 0)
            {
                CallFunc_IChatManager_GetConversation();
                return;
            }

            if (select_context.level2_item.CompareTo("GetConversationsFromServer") == 0)
            {
                CallFunc_IChatManager_GetConversationsFromServer();
                return;
            }

            if (select_context.level2_item.CompareTo("GetUnreadMessageCount") == 0)
            {
                CallFunc_IChatManager_GetUnreadMessageCount();
                return;
            }

            if (select_context.level2_item.CompareTo("ImportMessages") == 0)
            {
                CallFunc_IChatManager_ImportMessages();
                return;
            }

            if (select_context.level2_item.CompareTo("LoadAllConversations") == 0)
            {
                CallFunc_IChatManager_LoadAllConversations();
                return;
            }

            if (select_context.level2_item.CompareTo("LoadMessage") == 0)
            {
                CallFunc_IChatManager_LoadMessage();
                return;
            }

            if (select_context.level2_item.CompareTo("MarkAllConversationsAsRead") == 0)
            {
                CallFunc_IChatManager_MarkAllConversationsAsRead();
                return;
            }

            if (select_context.level2_item.CompareTo("RecallMessage") == 0)
            {
                CallFunc_IChatManager_RecallMessage();
                return;
            }

            if (select_context.level2_item.CompareTo("ResendMessage") == 0)
            {
                CallFunc_IChatManager_ResendMessage();
                return;
            }

            if (select_context.level2_item.CompareTo("SearchMsgFromDB") == 0)
            {
                CallFunc_IChatManager_SearchMsgFromDB();
                return;
            }

            if (select_context.level2_item.CompareTo("SendConversationReadAck") == 0)
            {
                CallFunc_IChatManager_SendConversationReadAck();
                return;
            }

            if (select_context.level2_item.CompareTo("SendTxtMessage") == 0)
            {
                CallFunc_IChatManager_SendTxtMessage();
                return;
            }

            if (select_context.level2_item.CompareTo("SendImageMessage") == 0)
            {
                CallFunc_IChatManager_SendImageMessage();
                return;
            }

            if (select_context.level2_item.CompareTo("SendFileMessage") == 0)
            {
                CallFunc_IChatManager_SendFileMessage();
                return;
            }

            if (select_context.level2_item.CompareTo("SendVideoMessage") == 0)
            {
                CallFunc_IChatManager_SendVideoMessage();
                return;
            }

            if (select_context.level2_item.CompareTo("SendVoiceMessage") == 0)
            {
                CallFunc_IChatManager_SendVoiceMessage();
                return;
            }

            if (select_context.level2_item.CompareTo("SendCmdMessage") == 0)
            {
                CallFunc_IChatManager_SendCmdMessage();
                return;
            }

            if (select_context.level2_item.CompareTo("SendCustomMessage") == 0)
            {
                CallFunc_IChatManager_SendCustomMessage();
                return;
            }

            if (select_context.level2_item.CompareTo("SendLocationMessage") == 0)
            {
                CallFunc_IChatManager_SendLocationMessage();
                return;
            }

            if (select_context.level2_item.CompareTo("SendMessageReadAck") == 0)
            {
                CallFunc_IChatManager_SendMessageReadAck();
                return;
            }

            if (select_context.level2_item.CompareTo("ChatManagerUpdateMessage") == 0)
            {
                CallFunc_IChatManager_ChatManagerUpdateMessage();
                return;
            }

            if (select_context.level2_item.CompareTo("RemoveMessagesBeforeTimestamp") == 0)
            {
                CallFunc_IChatManager_RemoveMessagesBeforeTimestamp();
                return;
            }

            if (select_context.level2_item.CompareTo("DeleteConversationFromServer") == 0)
            {
                CallFunc_IChatManager_DeleteConversationFromServer();
                return;
            }
        }

        public void CallFunc_IContactManager_AddContact(string _username = "", string _reason="")
        {
            string username = "";
            string reason = "";

            if (_username.Length > 0)
                username = _username;
            else
                username = GetParamValueFromContext(0);

            if (_reason.Length > 0)
                reason = _reason;
            else
                reason = GetParamValueFromContext(0);

            SDKClient.Instance.ContactManager.AddContact(username, reason, new CallBack(
                onSuccess: () =>
                {
                    Console.WriteLine($"AddContact success.");
                },
                onError: (code, desc) =>
                {
                    Console.WriteLine($"AddContact failed, code:{code}, desc:{desc}");
                }
            ));
        }

        public void CallFunc_IContactManager_DeleteContact(string _username = "", int _keep = -1)
        {
            string username = "";
            bool keep = false;

            if (_username.Length > 0)
                username = _username;
            else
                username = GetParamValueFromContext(0);

            if (0 == _keep || 1 == _keep)
                keep = (1 == _keep) ? true : false;
            else
            {
                int i = GetIntFromString(GetParamValueFromContext(1));
                if (0 == i || 1 == i)
                    keep = (1 == i) ? true : false;
            }

            SDKClient.Instance.ContactManager.DeleteContact(username, keep, new CallBack(
                onSuccess: () =>
                {
                    Console.WriteLine($"DeleteContact success.");
                },
                onError: (code, desc) =>
                {
                    Console.WriteLine($"DeleteContact failed, code:{code}, desc:{desc}");
                }
            ));
        }

        public void CallFunc_IContactManager_GetAllContactsFromServer()
        {
            SDKClient.Instance.ContactManager.GetAllContactsFromServer(new ValueCallBack<List<string>>(
            onSuccess: (list) =>
            {
                Console.WriteLine($"GetAllContactsFromServer success with contact num: {list.Count}.");
                foreach(var it in list)
                {
                    Console.WriteLine($"contactor: {it}");
                }
            },
            onError: (code, desc) =>
            {
                Console.WriteLine($"GetAllContactsFromServer failed, code:{code}, desc:{desc}");
            }
            ));
        }

        public void CallFunc_IContactManager_GetAllContactsFromDB()
        {
            List<string> list = SDKClient.Instance.ContactManager.GetAllContactsFromDB();

            Console.WriteLine($"GetAllContactsFromDB success with contact num: {list.Count}.");
            foreach (var it in list)
            {
                Console.WriteLine($"contactor: {it}");
            }
        }

        public void CallFunc_IContactManager_AddUserToBlockList(string _username = "")
        {
            string username = "";

            if (_username.Length > 0)
                username = _username;
            else
                username = GetParamValueFromContext(0);

            SDKClient.Instance.ContactManager.AddUserToBlockList(username, new CallBack(
                onSuccess: () =>
                {
                    Console.WriteLine($"AddUserToBlockList success.");
                },
                onError: (code, desc) =>
                {
                    Console.WriteLine($"AddUserToBlockList failed, code:{code}, desc:{desc}");
                }
            ));
        }

        public void CallFunc_IContactManager_RemoveUserFromBlockList(string _username = "")
        {
            string username = "";

            if (_username.Length > 0)
                username = _username;
            else
                username = GetParamValueFromContext(0);

            SDKClient.Instance.ContactManager.RemoveUserFromBlockList(username, new CallBack(
                onSuccess: () =>
                {
                    Console.WriteLine($"RemoveUserFromBlockList success.");
                },
                onError: (code, desc) =>
                {
                    Console.WriteLine($"RemoveUserFromBlockList failed, code:{code}, desc:{desc}");
                }
            ));
        }

        public void CallFunc_IContactManager_GetBlockListFromServer()
        {
            SDKClient.Instance.ContactManager.GetBlockListFromServer(new ValueCallBack<List<string>>(
            onSuccess: (list) =>
            {
                Console.WriteLine($"GetBlockListFromServer success with contact num: {list.Count}.");
                foreach (var it in list)
                {
                    Console.WriteLine($"block member: {it}");
                }
            },
            onError: (code, desc) =>
            {
                Console.WriteLine($"GetBlockListFromServer failed, code:{code}, desc:{desc}");
            }
            ));
        }

        public void CallFunc_IContactManager_AcceptInvitation(string _username = "")
        {
            string username = "";

            if (_username.Length > 0)
                username = _username;
            else
                username = GetParamValueFromContext(0);

            SDKClient.Instance.ContactManager.AcceptInvitation(username, new CallBack(
                onSuccess: () =>
                {
                    Console.WriteLine($"AcceptInvitation success.");
                },
                onError: (code, desc) =>
                {
                    Console.WriteLine($"AcceptInvitation failed, code:{code}, desc:{desc}");
                }
            ));
        }

        public void CallFunc_IContactManager_DeclineInvitation(string _username = "")
        {
            string username = "";

            if (_username.Length > 0)
                username = _username;
            else
                username = GetParamValueFromContext(0);

            SDKClient.Instance.ContactManager.DeclineInvitation(username, new CallBack(
                onSuccess: () =>
                {
                    Console.WriteLine($"DeclineInvitation success.");
                },
                onError: (code, desc) =>
                {
                    Console.WriteLine($"DeclineInvitation failed, code:{code}, desc:{desc}");
                }
            ));
        }

        public void CallFunc_IContactManager_GetSelfIdsOnOtherPlatform()
        {
            SDKClient.Instance.ContactManager.GetSelfIdsOnOtherPlatform(new ValueCallBack<List<string>>(
            onSuccess: (list) =>
            {
                Console.WriteLine($"GetSelfIdsOnOtherPlatform success with contact num: {list.Count}.");
                foreach (var it in list)
                {
                    Console.WriteLine($"self id: {it}");
                }
            },
            onError: (code, desc) =>
            {
                Console.WriteLine($"GetSelfIdsOnOtherPlatform failed, code:{code}, desc:{desc}");
            }
            ));
        }

        public void CallFunc_IContactManager()
        {
            if (select_context.level2_item.CompareTo("AddContact") == 0)
            {
                CallFunc_IContactManager_AddContact();
                return;
            }

            if (select_context.level2_item.CompareTo("DeleteContact") == 0)
            {
                CallFunc_IContactManager_DeleteContact();
                return;
            }

            if (select_context.level2_item.CompareTo("GetAllContactsFromServer") == 0)
            {
                CallFunc_IContactManager_GetAllContactsFromServer();
                return;
            }

            if (select_context.level2_item.CompareTo("GetAllContactsFromDB") == 0)
            {
                CallFunc_IContactManager_GetAllContactsFromDB();
                return;
            }

            if (select_context.level2_item.CompareTo("AddUserToBlockList") == 0)
            {
                CallFunc_IContactManager_AddUserToBlockList();
                return;
            }

            if (select_context.level2_item.CompareTo("RemoveUserFromBlockList") == 0)
            {
                CallFunc_IContactManager_RemoveUserFromBlockList();
                return;
            }

            if (select_context.level2_item.CompareTo("GetBlockListFromServer") == 0)
            {
                CallFunc_IContactManager_GetBlockListFromServer();
                return;
            }

            if (select_context.level2_item.CompareTo("AcceptInvitation") == 0)
            {
                CallFunc_IContactManager_AcceptInvitation();
                return;
            }

            if (select_context.level2_item.CompareTo("DeclineInvitation") == 0)
            {
                CallFunc_IContactManager_DeclineInvitation();
                return;
            }

            if (select_context.level2_item.CompareTo("GetSelfIdsOnOtherPlatform") == 0)
            {
                CallFunc_IContactManager_GetSelfIdsOnOtherPlatform();
                return;
            }
        }

        public void CallFunc_IConversationManager_LastMessage(string _cid = "", int _type = -1)
        {
            string cid = "";
            ConversationType type = ConversationType.Chat;

            if (_cid.Length > 0)
                cid = _cid;
            else
                cid = GetParamValueFromContext(0);

            if (_type >= 0 && _type <= 2)
                type = (ConversationType)_type;
            else
            {
                int i = GetIntFromString(GetParamValueFromContext(1));
                if (i >= 0 && i <= 2)
                    type = (ConversationType)i;
            }

            Conversation conv = SDKClient.Instance.ChatManager.GetConversation(cid, type);

            if (conv.LastMessage != null)
            {
                Console.WriteLine($"LastMessage success, msgid: {conv.LastMessage.MsgId}.");
            }
            else
            {
                Console.WriteLine($"LastMessage failed.");
            }
        }

        public void CallFunc_IConversationManager_LastReceivedMessage(string _cid = "", int _type = -1)
        {
            string cid = "";
            ConversationType type = ConversationType.Chat;

            if (_cid.Length > 0)
                cid = _cid;
            else
                cid = GetParamValueFromContext(0);

            if (_type >= 0 && _type <= 2)
                type = (ConversationType)_type;
            else
            {
                int i = GetIntFromString(GetParamValueFromContext(1));
                if (i >= 0 && i <= 2)
                    type = (ConversationType)i;
            }

            Conversation conv = SDKClient.Instance.ChatManager.GetConversation(cid, type);

            if (conv.LastReceivedMessage != null)
            {
                Console.WriteLine($"LastReceivedMessage success, msgid: {conv.LastMessage.MsgId}.");
            }
            else
            {
                Console.WriteLine($"LastReceivedMessage failed.");
            }
        }

        public void CallFunc_IConversationManager_GetExt(string _cid = "", int _type = -1)
        {
            string cid = "";
            ConversationType type = ConversationType.Chat;

            if (_cid.Length > 0)
                cid = _cid;
            else
                cid = GetParamValueFromContext(0);

            if (_type >= 0 && _type <= 2)
                type = (ConversationType)_type;
            else
            {
                int i = GetIntFromString(GetParamValueFromContext(1));
                if (i >= 0 && i <= 2)
                    type = (ConversationType)i;
            }

            Conversation conv = SDKClient.Instance.ChatManager.GetConversation(cid, type);

            Dictionary<string, string> dict = conv.Ext;

            if (dict != null)
            {
                Console.WriteLine($"GetExt success, dict count: {dict.Count}.");
                foreach(var it in dict)
                {
                    Console.WriteLine($"Ext item: key:{it.Key}, value:{it.Value}.");
                }
            }
            else
            {
                Console.WriteLine($"GetExt failed.");
            }
        }

        public void CallFunc_IConversationManager_SetExt(string _cid = "", int _type = -1)
        {
            string cid = "";
            ConversationType type = ConversationType.Chat;

            if (_cid.Length > 0)
                cid = _cid;
            else
                cid = GetParamValueFromContext(0);

            if (_type >= 0 && _type <= 2)
                type = (ConversationType)_type;
            else
            {
                int i = GetIntFromString(GetParamValueFromContext(1));
                if (i >= 0 && i <= 2)
                    type = (ConversationType)i;
            }

            Conversation conv = SDKClient.Instance.ChatManager.GetConversation(cid, type);

            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("key1", "value1");
            dict.Add("key2", "value2");
            dict.Add("key3", "value3");

            conv.Ext = dict;
            Console.WriteLine($"SetExt comppleted.");
        }

        public void CallFunc_IConversationManager_UnReadCount(string _cid = "", int _type = -1)
        {
            string cid = "";
            ConversationType type = ConversationType.Chat;

            if (_cid.Length > 0)
                cid = _cid;
            else
                cid = GetParamValueFromContext(0);

            if (_type >= 0 && _type <= 2)
                type = (ConversationType)_type;
            else
            {
                int i = GetIntFromString(GetParamValueFromContext(1));
                if (i >= 0 && i <= 2)
                    type = (ConversationType)i;
            }

            Conversation conv = SDKClient.Instance.ChatManager.GetConversation(cid, type);

            int count = conv.UnReadCount;
            Console.WriteLine($"UnReadCount is:{count}.");
        }

        public void CallFunc_IConversationManager_MessagesCount(string _cid = "", int _type = -1)
        {
            string cid = "";
            ConversationType type = ConversationType.Chat;

            if (_cid.Length > 0)
                cid = _cid;
            else
                cid = GetParamValueFromContext(0);

            if (_type >= 0 && _type <= 2)
                type = (ConversationType)_type;
            else
            {
                int i = GetIntFromString(GetParamValueFromContext(1));
                if (i >= 0 && i <= 2)
                    type = (ConversationType)i;
            }

            Conversation conv = SDKClient.Instance.ChatManager.GetConversation(cid, type);

            int count = conv.MessagesCount();
            Console.WriteLine($"MessagesCount is:{count}.");
        }

        public void CallFunc_IConversationManager_MarkMessageAsRead(string _cid = "", int _type = -1, string _msgid="")
        {
            string cid = "";
            ConversationType type = ConversationType.Chat;
            string msgid = "";

            if (_cid.Length > 0)
                cid = _cid;
            else
                cid = GetParamValueFromContext(0);

            if (_type >= 0 && _type <= 2)
                type = (ConversationType)_type;
            else
            {
                int i = GetIntFromString(GetParamValueFromContext(1));
                if (i >= 0 && i <= 2)
                    type = (ConversationType)i;
            }

            if (_msgid.Length > 0)
                msgid = _msgid;
            else
                msgid = GetParamValueFromContext(2);

            Conversation conv = SDKClient.Instance.ChatManager.GetConversation(cid, type);

            conv.MarkMessageAsRead(msgid);
            Console.WriteLine($"MarkMessageAsRead done.");
        }

        public void CallFunc_IConversationManager_MarkAllMessageAsRead(string _cid = "", int _type = -1)
        {
            string cid = "";
            ConversationType type = ConversationType.Chat;

            if (_cid.Length > 0)
                cid = _cid;
            else
                cid = GetParamValueFromContext(0);

            if (_type >= 0 && _type <= 2)
                type = (ConversationType)_type;
            else
            {
                int i = GetIntFromString(GetParamValueFromContext(1));
                if (i >= 0 && i <= 2)
                    type = (ConversationType)i;
            }

            Conversation conv = SDKClient.Instance.ChatManager.GetConversation(cid, type);

            conv.MarkAllMessageAsRead();
            Console.WriteLine($"MarkAllMessageAsRead done.");
        }

        public void CallFunc_IConversationManager_InsertMessage(string _cid = "", int _type = -1)
        {
            string cid = "";
            ConversationType type = ConversationType.Chat;

            if (_cid.Length > 0)
                cid = _cid;
            else
                cid = GetParamValueFromContext(0);

            if (_type >= 0 && _type <= 2)
                type = (ConversationType)_type;
            else
            {
                int i = GetIntFromString(GetParamValueFromContext(1));
                if (i >= 0 && i <= 2)
                    type = (ConversationType)i;
            }

            Conversation conv = SDKClient.Instance.ChatManager.GetConversation(cid, type);

            string str = "insertmessage " + GetTS().ToString();
            Message msg = Message.CreateTextSendMessage("you", str);

            if (conv.InsertMessage(msg))
                Console.WriteLine($"InsertMessage success.");
            else
                Console.WriteLine($"InsertMessage failed.");
        }

        public void CallFunc_IConversationManager_AppendMessage(string _cid = "", int _type = -1)
        {
            string cid = "";
            ConversationType type = ConversationType.Chat;

            if (_cid.Length > 0)
                cid = _cid;
            else
                cid = GetParamValueFromContext(0);

            if (_type >= 0 && _type <= 2)
                type = (ConversationType)_type;
            else
            {
                int i = GetIntFromString(GetParamValueFromContext(1));
                if (i >= 0 && i <= 2)
                    type = (ConversationType)i;
            }

            Conversation conv = SDKClient.Instance.ChatManager.GetConversation(cid, type);

            string str = "appendmessage " + GetTS().ToString();
            Message msg = Message.CreateTextSendMessage("you", str);

            if (conv.AppendMessage(msg))
                Console.WriteLine($"AppendMessage success.");
            else
                Console.WriteLine($"AppendMessage failed.");
        }

        public void CallFunc_IConversationManager_UpdateMessage(string _cid = "", int _type = -1, string _msgid="", int _status = -1)
        {
            string cid = "";
            ConversationType type = ConversationType.Chat;
            string msgid = "";
            MessageStatus status = MessageStatus.CREATE;

            if (_cid.Length > 0)
                cid = _cid;
            else
                cid = GetParamValueFromContext(0);

            if (_type >= 0 && _type <= 2)
                type = (ConversationType)_type;
            else
            {
                int i = GetIntFromString(GetParamValueFromContext(1));
                if (i >= 0 && i <= 2)
                    type = (ConversationType)i;
            }

            if (_msgid.Length > 0)
                msgid = _msgid;
            else
                msgid = GetParamValueFromContext(2);

            Message msg = SDKClient.Instance.ChatManager.LoadMessage(msgid);
            if (null == msg)
            {
                Console.WriteLine($"UpdateMessage cannot find the message with id {msgid}.");
                return;
            }

            if (_status >= 0 && _status <= 3)
                status = (MessageStatus)_status;
            else
            {
                int i = GetIntFromString(GetParamValueFromContext(3));
                if (i >= 0 && i <= 3)
                    status = (MessageStatus)i;
            }

            Conversation conv = SDKClient.Instance.ChatManager.GetConversation(cid, type);

            if (conv.UpdateMessage(msg))
                Console.WriteLine($"UpdateMessage success.");
            else
                Console.WriteLine($"UpdateMessage failed.");
        }

        public void CallFunc_IConversationManager_DeleteMessage(string _cid = "", int _type = -1, string _msgid="")
        {
            string cid = "";
            ConversationType type = ConversationType.Chat;
            string msgid = "";

            if (_cid.Length > 0)
                cid = _cid;
            else
                cid = GetParamValueFromContext(0);

            if (_type >= 0 && _type <= 2)
                type = (ConversationType)_type;
            else
            {
                int i = GetIntFromString(GetParamValueFromContext(1));
                if (i >= 0 && i <= 2)
                    type = (ConversationType)i;
            }

            if (_msgid.Length > 0)
                msgid = _msgid;
            else
                msgid = GetParamValueFromContext(2);

            Conversation conv = SDKClient.Instance.ChatManager.GetConversation(cid, type);

            if (conv.DeleteMessage(msgid))
                Console.WriteLine($"DeleteMessage success.");
            else
                Console.WriteLine($"DeleteMessage failed.");
        }

        public void CallFunc_IConversationManager_DeleteAllMessages(string _cid = "", int _type = -1)
        {
            string cid = "";
            ConversationType type = ConversationType.Chat;

            if (_cid.Length > 0)
                cid = _cid;
            else
                cid = GetParamValueFromContext(0);

            if (_type >= 0 && _type <= 2)
                type = (ConversationType)_type;
            else
            {
                int i = GetIntFromString(GetParamValueFromContext(1));
                if (i >= 0 && i <= 2)
                    type = (ConversationType)i;
            }

            Conversation conv = SDKClient.Instance.ChatManager.GetConversation(cid, type);

            if (conv.DeleteAllMessages())
                Console.WriteLine($"DeleteAllMessages success.");
            else
                Console.WriteLine($"DeleteAllMessages failed.");
        }

        public void CallFunc_IConversationManager_LoadMessage(string _cid = "", int _type = -1, string _msgid = "")
        {
            string cid = "";
            ConversationType type = ConversationType.Chat;
            string msgid = "";

            if (_cid.Length > 0)
                cid = _cid;
            else
                cid = GetParamValueFromContext(0);

            if (_type >= 0 && _type <= 2)
                type = (ConversationType)_type;
            else
            {
                int i = GetIntFromString(GetParamValueFromContext(1));
                if (i >= 0 && i <= 2)
                    type = (ConversationType)i;
            }

            if (_msgid.Length > 0)
                msgid = _msgid;
            else
                msgid = GetParamValueFromContext(2);

            Conversation conv = SDKClient.Instance.ChatManager.GetConversation(cid, type);

            Message msg = conv.LoadMessage(msgid);
            if(null != msg)
                Console.WriteLine($"LoadMessage success.");
            else
                Console.WriteLine($"LoadMessage failed.");
        }

        public void CallFunc_IConversationManager_LoadMessagesWithMsgType(string _cid = "", int _type = -1, int _bodytype = -1, string _sender = "", long _ts = -1, int _count = -1, int _direct = -1)
        {
            string cid = "";
            ConversationType type = ConversationType.Chat;
            MessageBodyType bodyType = MessageBodyType.TXT;
            string sender = "";
            long ts = -1;
            int count = -1;
            MessageSearchDirection direct = MessageSearchDirection.UP;

            if (_cid.Length > 0)
                cid = _cid;
            else
                cid = GetParamValueFromContext(0);

            if (_type >= 0 && _type <= 2)
                type = (ConversationType)_type;
            else
            {
                int i = GetIntFromString(GetParamValueFromContext(1));
                if (i >= 0 && i <= 2)
                    type = (ConversationType)i;
            }

            if (_bodytype >= 0 && _bodytype <= 7)
                bodyType = (MessageBodyType)_bodytype;
            else
            {
                int i = GetIntFromString(GetParamValueFromContext(2));
                if (i >= 0 && i <= 7)
                    bodyType = (MessageBodyType)i;
            }

            if (_sender.Length > 0)
                sender = _sender;
            else
                sender = GetParamValueFromContext(3);

            if (-1 != _ts)
                ts = _ts;
            else
                ts = GetLongFromString(GetParamValueFromContext(4));

            if (-1 != _count)
                count = _count;
            else
                count = GetIntFromString(GetParamValueFromContext(5));

            if (0 == _direct || 1 == _direct)
                direct = (MessageSearchDirection)_direct;
            else
            {
                int i = GetIntFromString(GetParamValueFromContext(6));
                if (0 == i || 1 == i)
                    direct = (MessageSearchDirection)i;
            }

            Conversation conv = SDKClient.Instance.ChatManager.GetConversation(cid, type);
     
            conv.LoadMessagesWithMsgType(bodyType, sender, (int)ts, count, direct, new ValueCallBack<List<Message>>(
                onSuccess: (list) => {
                    Console.WriteLine($"LoadMessagesWithMsgType found {list.Count} messages");
                    foreach(var it in list)
                    {
                        Console.WriteLine($"message id: {it.MsgId}");
                    }
                },
                onError: (code, desc) => {
                    Console.WriteLine($"LoadMessagesWithMsgType failed, code:{code}, desc:{desc}");
                }
            ));
        }

        public void CallFunc_IConversationManager_LoadMessages(string _cid = "", int _type = -1, string _startMsgId = "", int _count = -1, int _direct = -1)
        {
            string cid = "";
            ConversationType type = ConversationType.Chat;
            MessageBodyType bodyType = MessageBodyType.TXT;
            string startMsgId = "";
            long ts = -1;
            int count = -1;
            MessageSearchDirection direct = MessageSearchDirection.UP;

            if (_cid.Length > 0)
                cid = _cid;
            else
                cid = GetParamValueFromContext(0);

            if (_type >= 0 && _type <= 2)
                type = (ConversationType)_type;
            else
            {
                int i = GetIntFromString(GetParamValueFromContext(1));
                if (i >= 0 && i <= 2)
                    type = (ConversationType)i;
            }

            if (_startMsgId.Length > 0)
                startMsgId = _startMsgId;
            else
                startMsgId = GetParamValueFromContext(2);

            if (-1 != _count)
                count = _count;
            else
                count = GetIntFromString(GetParamValueFromContext(3));

            if (0 == _direct || 1 == _direct)
                direct = (MessageSearchDirection)_direct;
            else
            {
                int i = GetIntFromString(GetParamValueFromContext(4));
                if (0 == i || 1 == i)
                    direct = (MessageSearchDirection)i;
            }

            Conversation conv = SDKClient.Instance.ChatManager.GetConversation(cid, type);

            conv.LoadMessages(startMsgId, count, direct, new ValueCallBack<List<Message>>(
                onSuccess: (list) => {
                    Console.WriteLine($"LoadMessages found {list.Count} messages");
                    foreach (var it in list)
                    {
                        Console.WriteLine($"message id: {it.MsgId}");
                    }
                },
                onError: (code, desc) => {
                    Console.WriteLine($"LoadMessages failed, code:{code}, desc:{desc}");
                }
            ));
        }

        public void CallFunc_IConversationManager_LoadMessagesWithKeyword(string _cid = "", int _type = -1, string _keywords="", string _sender = "", long _ts = -1, int _count = -1, int _direct = -1)
        {
            string cid = "";
            ConversationType type = ConversationType.Chat;
            string keywords = "";
            string sender = "";
            long ts = -1;
            int count = -1;
            MessageSearchDirection direct = MessageSearchDirection.UP;

            if (_cid.Length > 0)
                cid = _cid;
            else
                cid = GetParamValueFromContext(0);

            if (_type >= 0 && _type <= 2)
                type = (ConversationType)_type;
            else
            {
                int i = GetIntFromString(GetParamValueFromContext(1));
                if (i >= 0 && i <= 2)
                    type = (ConversationType)i;
            }

            if (_keywords.Length > 0)
                keywords = _keywords;
            else
                keywords = GetParamValueFromContext(2);

            if (_sender.Length > 0)
                sender = _sender;
            else
                sender = GetParamValueFromContext(3);

            if (-1 != _ts)
                ts = _ts;
            else
                ts = GetLongFromString(GetParamValueFromContext(4));

            if (-1 != _count)
                count = _count;
            else
                count = GetIntFromString(GetParamValueFromContext(5));

            if (0 == _direct || 1 == _direct)
                direct = (MessageSearchDirection)_direct;
            else
            {
                int i = GetIntFromString(GetParamValueFromContext(6));
                if (0 == i || 1 == i)
                    direct = (MessageSearchDirection)i;
            }

            Conversation conv = SDKClient.Instance.ChatManager.GetConversation(cid, type);

            conv.LoadMessagesWithKeyword(keywords, sender, ts, count, direct, new ValueCallBack<List<Message>>(
                onSuccess: (list) => {
                    Console.WriteLine($"LoadMessagesWithKeyword found {list.Count} messages");
                    foreach (var it in list)
                    {
                        Console.WriteLine($"message id: {it.MsgId}");
                    }
                },
                onError: (code, desc) => {
                    Console.WriteLine($"LoadMessagesWithKeyword failed, code:{code}, desc:{desc}");
                }
            ));
        }

        public void CallFunc_IConversationManager_LoadMessagesWithTime(string _cid = "", int _type = -1, long _start_ts=-1, long _end_ts=-1, int _count = -1)
        {
            string cid = "";
            ConversationType type = ConversationType.Chat;
            long start_ts = -1;
            long end_ts = -1;
            int count = -1;

            if (_cid.Length > 0)
                cid = _cid;
            else
                cid = GetParamValueFromContext(0);

            if (_type >= 0 && _type <= 2)
                type = (ConversationType)_type;
            else
            {
                int i = GetIntFromString(GetParamValueFromContext(1));
                if (i >= 0 && i <= 2)
                    type = (ConversationType)i;
            }

            if (-1 != _start_ts)
                start_ts = _start_ts;
            else
                start_ts = GetLongFromString(GetParamValueFromContext(2));

            if (-1 != _end_ts)
                end_ts = _end_ts;
            else
                end_ts = GetLongFromString(GetParamValueFromContext(3));

            if (-1 != _count)
                count = _count;
            else
                count = GetIntFromString(GetParamValueFromContext(4));

            Conversation conv = SDKClient.Instance.ChatManager.GetConversation(cid, type);

            conv.LoadMessagesWithTime(start_ts, end_ts, count, new ValueCallBack<List<Message>>(
                onSuccess: (list) => {
                    Console.WriteLine($"LoadMessagesWithTime found {list.Count} messages");
                    foreach (var it in list)
                    {
                        Console.WriteLine($"message id: {it.MsgId}");
                    }
                },
                onError: (code, desc) => {
                    Console.WriteLine($"LoadMessagesWithTime failed, code:{code}, desc:{desc}");
                }
            ));
        }

        public void CallFunc_IConversationManager()
        {
            if (select_context.level2_item.CompareTo("LastMessage") == 0)
            {
                CallFunc_IConversationManager_LastMessage();
                return;
            }

            if (select_context.level2_item.CompareTo("LastReceivedMessage") == 0)
            {
                CallFunc_IConversationManager_LastReceivedMessage();
                return;
            }

            if (select_context.level2_item.CompareTo("GetExt") == 0)
            {
                CallFunc_IConversationManager_GetExt();
                return;
            }

            if (select_context.level2_item.CompareTo("SetExt") == 0)
            {
                CallFunc_IConversationManager_SetExt();
                return;
            }

            if (select_context.level2_item.CompareTo("UnReadCount") == 0)
            {
                CallFunc_IConversationManager_UnReadCount();
                return;
            }

            if (select_context.level2_item.CompareTo("MessagesCount") == 0)
            {
                CallFunc_IConversationManager_MessagesCount();
                return;
            }

            if (select_context.level2_item.CompareTo("MarkMessageAsRead") == 0)
            {
                CallFunc_IConversationManager_MarkMessageAsRead();
                return;
            }

            if (select_context.level2_item.CompareTo("MarkAllMessageAsRead") == 0)
            {
                CallFunc_IConversationManager_MarkAllMessageAsRead();
                return;
            }

            if (select_context.level2_item.CompareTo("InsertMessage") == 0)
            {
                CallFunc_IConversationManager_InsertMessage();
                return;
            }

            if (select_context.level2_item.CompareTo("AppendMessage") == 0)
            {
                CallFunc_IConversationManager_AppendMessage();
                return;
            }

            if (select_context.level2_item.CompareTo("UpdateMessage") == 0)
            {
                CallFunc_IConversationManager_UpdateMessage();
                return;
            }

            if (select_context.level2_item.CompareTo("DeleteMessage") == 0)
            {
                CallFunc_IConversationManager_DeleteMessage();
                return;
            }

            if (select_context.level2_item.CompareTo("DeleteAllMessages") == 0)
            {
                CallFunc_IConversationManager_DeleteAllMessages();
                return;
            }

            if (select_context.level2_item.CompareTo("LoadConverationMessage") == 0)
            {
                CallFunc_IConversationManager_LoadMessage();
                return;
            }

            if (select_context.level2_item.CompareTo("LoadMessagesWithMsgType") == 0)
            {
                CallFunc_IConversationManager_LoadMessagesWithMsgType();
                return;
            }

            if (select_context.level2_item.CompareTo("LoadMessages") == 0)
            {
                CallFunc_IConversationManager_LoadMessages();
                return;
            }

            if (select_context.level2_item.CompareTo("LoadMessagesWithKeyword") == 0)
            {
                CallFunc_IConversationManager_LoadMessagesWithKeyword();
                return;
            }

            if (select_context.level2_item.CompareTo("LoadMessagesWithTime") == 0)
            {
                CallFunc_IConversationManager_LoadMessagesWithTime();
                return;
            }
        }

        public void CallFunc_IGroupManager_applyJoinToGroup(string _groupId = "", string _reason = "")
        {
            string groupId = "";
            string reason = "";

            if (_groupId.Length > 0)
                groupId = _groupId;
            else
                groupId = GetParamValueFromContext(0);

            if (_reason.Length > 0)
                reason = _reason;
            else
                reason = GetParamValueFromContext(1);

            SDKClient.Instance.GroupManager.applyJoinToGroup(groupId, reason, new CallBack(
                onSuccess: () =>
                {
                    Console.WriteLine($"applyJoinToGroup success.");
                },
                onError: (code, desc) =>
                {
                    Console.WriteLine($"applyJoinToGroup failed, code:{code}, desc:{desc}");
                }
            ));
        }

        public void CallFunc_IGroupManager_AcceptGroupInvitation(string _groupId = "")
        {
            Console.WriteLine("AcceptGroupInvitation only can be called in OnInvitationReceivedFromGroup. ");
            Console.WriteLine($"Here only set to accept invitation for group id:{_groupId}");
            select_context.group_invitation_select[_groupId] = 1;
        }

        public void CallFunc_IGroupManager_AcceptGroupJoinApplication(string _groupId = "")
        {
            Console.WriteLine("AcceptGroupJoinApplication only can be called in OnRequestToJoinReceivedFromGroup. ");
            Console.WriteLine($"Here only set to accept application for group id:{_groupId}");
            select_context.group_application_select[_groupId] = 1;
        }

        public void CallFunc_IGroupManager_AddGroupAdmin(string _groupId = "", string _memberId="")
        {
            string groupId = "";
            string memberId = "";

            if (_groupId.Length > 0)
                groupId = _groupId;
            else
                groupId = GetParamValueFromContext(0);

            if (_memberId.Length > 0)
                memberId = _memberId;
            else
                memberId = GetParamValueFromContext(1);

            SDKClient.Instance.GroupManager.AddGroupAdmin(groupId, memberId, new CallBack(
                onSuccess: () =>
                {
                    Console.WriteLine($"AddGroupAdmin success.");
                },
                onError: (code, desc) =>
                {
                    Console.WriteLine($"AddGroupAdmin failed, code:{code}, desc:{desc}");
                }
            ));
        }

        public void CallFunc_IGroupManager_AddGroupMembers(string _groupId = "", string _memberId1 = "", string _memberId2 = "", string _memberId3 = "")
        {
            string groupId = "";
            string memberId1 = "";
            string memberId2 = "";
            string memberId3 = "";


            if (_groupId.Length > 0)
                groupId = _groupId;
            else
                groupId = GetParamValueFromContext(0);

            if (_memberId1.Length > 0)
                memberId1 = _memberId1;
            else
                memberId1 = GetParamValueFromContext(1);

            if (_memberId2.Length > 0)
                memberId2 = _memberId2;
            else
                memberId2 = GetParamValueFromContext(2);

            if (_memberId3.Length > 0)
                memberId3 = _memberId3;
            else
                memberId3 = GetParamValueFromContext(3);

            List<string> members = new List<string>();
            members.Add(memberId1);
            members.Add(memberId2);
            members.Add(memberId3);

            SDKClient.Instance.GroupManager.AddGroupMembers(groupId, members, new CallBack(
                onSuccess: () =>
                {
                    Console.WriteLine($"AddGroupMembers success.");
                },
                onError: (code, desc) =>
                {
                    Console.WriteLine($"AddGroupMembers failed, code:{code}, desc:{desc}");
                }
            ));
        }

        public void CallFunc_IGroupManager_AddGroupWhiteList(string _groupId = "", string _memberId1 = "", string _memberId2 = "", string _memberId3 = "")
        {
            string groupId = "";
            string memberId1 = "";
            string memberId2 = "";
            string memberId3 = "";

            if (_groupId.Length > 0)
                groupId = _groupId;
            else
                groupId = GetParamValueFromContext(0);

            if (_memberId1.Length > 0)
                memberId1 = _memberId1;
            else
                memberId1 = GetParamValueFromContext(1);

            if (_memberId2.Length > 0)
                memberId2 = _memberId2;
            else
                memberId2 = GetParamValueFromContext(1);

            if (_memberId3.Length > 0)
                memberId3 = _memberId3;
            else
                memberId2 = GetParamValueFromContext(1);

            List<string> members = new List<string>();
            members.Add(memberId1);
            members.Add(memberId2);
            members.Add(memberId3);

            SDKClient.Instance.GroupManager.AddGroupWhiteList(groupId, members, new CallBack(
                onSuccess: () =>
                {
                    Console.WriteLine($"AddGroupWhiteList success.");
                },
                onError: (code, desc) =>
                {
                    Console.WriteLine($"AddGroupWhiteList failed, code:{code}, desc:{desc}");
                }
            ));
        }

        public void CallFunc_IGroupManager_BlockGroup(string _groupId = "")
        {
            string groupId = "";

            if (_groupId.Length > 0)
                groupId = _groupId;
            else
                groupId = GetParamValueFromContext(0);

            SDKClient.Instance.GroupManager.BlockGroup(groupId, new CallBack(
                onSuccess: () =>
                {
                    Console.WriteLine($"BlockGroup success.");
                },
                onError: (code, desc) =>
                {
                    Console.WriteLine($"BlockGroup failed, code:{code}, desc:{desc}");
                }
            ));
        }

        public void CallFunc_IGroupManager_BlockGroupMembers(string _groupId = "", string _memberId1 = "", string _memberId2 = "", string _memberId3 = "")
        {
            string groupId = "";
            string memberId1 = "";
            string memberId2 = "";
            string memberId3 = "";

            if (_groupId.Length > 0)
                groupId = _groupId;
            else
                groupId = GetParamValueFromContext(0);

            if (_memberId1.Length > 0)
                memberId1 = _memberId1;
            else
                memberId1 = GetParamValueFromContext(1);

            if (_memberId2.Length > 0)
                memberId2 = _memberId2;
            else
                memberId2 = GetParamValueFromContext(1);

            if (_memberId3.Length > 0)
                memberId3 = _memberId3;
            else
                memberId2 = GetParamValueFromContext(1);

            List<string> members = new List<string>();
            members.Add(memberId1);
            members.Add(memberId2);
            members.Add(memberId3);

            SDKClient.Instance.GroupManager.BlockGroupMembers(groupId, members, new CallBack(
                onSuccess: () =>
                {
                    Console.WriteLine($"BlockGroupMembers success.");
                },
                onError: (code, desc) =>
                {
                    Console.WriteLine($"BlockGroupMembers failed, code:{code}, desc:{desc}");
                }
            ));
        }

        public void CallFunc_IGroupManager_ChangeGroupDescription(string _groupId = "", string _desc = "")
        {
            string groupId = "";
            string groupdesc = "";

            if (_groupId.Length > 0)
                groupId = _groupId;
            else
                groupId = GetParamValueFromContext(0);

            if (_desc.Length > 0)
                groupdesc = _desc;
            else
                groupdesc = GetParamValueFromContext(1);

            SDKClient.Instance.GroupManager.ChangeGroupDescription(groupId, groupdesc, new CallBack(
                onSuccess: () =>
                {
                    Console.WriteLine($"ChangeGroupDescription success.");
                },
                onError: (code, desc) =>
                {
                    Console.WriteLine($"ChangeGroupDescription failed, code:{code}, desc:{desc}");
                }
            ));
        }

        public void CallFunc_IGroupManager_ChangeGroupName(string _groupId = "", string _name = "")
        {
            string groupId = "";
            string name = "";

            if (_groupId.Length > 0)
                groupId = _groupId;
            else
                groupId = GetParamValueFromContext(0);

            if (_name.Length > 0)
                name = _name;
            else
                name = GetParamValueFromContext(1);

            SDKClient.Instance.GroupManager.ChangeGroupName(groupId, name, new CallBack(
                onSuccess: () =>
                {
                    Console.WriteLine($"ChangeGroupName success.");
                },
                onError: (code, desc) =>
                {
                    Console.WriteLine($"ChangeGroupName failed, code:{code}, desc:{desc}");
                }
            ));
        }

        public void CallFunc_IGroupManager_ChangeGroupOwner(string _groupId = "", string _owner = "")
        {
            string groupId = "";
            string owner = "";

            if (_groupId.Length > 0)
                groupId = _groupId;
            else
                groupId = GetParamValueFromContext(0);

            if (_owner.Length > 0)
                owner = _owner;
            else
                owner = GetParamValueFromContext(1);

            SDKClient.Instance.GroupManager.ChangeGroupOwner(groupId, owner, new CallBack(
                onSuccess: () =>
                {
                    Console.WriteLine($"ChangeGroupOwner success.");
                },
                onError: (code, desc) =>
                {
                    Console.WriteLine($"ChangeGroupOwner failed, code:{code}, desc:{desc}");
                }
            ));
        }

        public void CallFunc_IGroupManager_CheckIfInGroupWhiteList(string _groupId = "")
        {
            string groupId = "";

            if (_groupId.Length > 0)
                groupId = _groupId;
            else
                groupId = GetParamValueFromContext(0);


            SDKClient.Instance.GroupManager.CheckIfInGroupWhiteList(groupId, new ValueCallBack<bool>(
               onSuccess: (ret) => {
                   Console.WriteLine($"CheckIfInGroupWhiteList success, ret:{ret}");
               },
               onError: (code, desc) => {
                   Console.WriteLine($"CheckIfInGroupWhiteList failed, code:{code}, desc:{desc}");
               }
            ));
        }

        public void CallFunc_IGroupManager_CreateGroup(string _groupId = "", string _desc = "",string _member1="", string _member2="", string _reason="")
        {
            string groupId = "";
            string desc = "";
            string member1 = "";
            string member2 = "";
            string reason = "";

            if (_groupId.Length > 0)
                groupId = _groupId;
            else
                groupId = GetParamValueFromContext(0);

            if (_desc.Length > 0)
                desc = _desc;
            else
                desc = GetParamValueFromContext(1);

            if (_member1.Length > 0)
                member1 = _member1;
            else
                member1 = GetParamValueFromContext(2);

            if (_member2.Length > 0)
                member2 = _member2;
            else
                member2 = GetParamValueFromContext(3);

            if (_reason.Length > 0)
                reason = _reason;
            else
                reason = GetParamValueFromContext(4);

            List<string> members = new List<string>();
            members.Add(member1);
            members.Add(member2);

            GroupOptions gp = new GroupOptions(GroupStyle.PrivateMemberCanInvite);

            SDKClient.Instance.GroupManager.CreateGroup(groupId, gp, desc, members, reason, new ValueCallBack<Group>(
                onSuccess: (group) => {
                    Console.WriteLine($"CreateGroup success, groupId:{group.GroupId}, desc:{group.Description}");
                },
                onError: (code, error) => {
                    Console.WriteLine($"CreateGroup failed, code:{code}, desc:{desc}");
                }
            ));
        }

        public void CallFunc_IGroupManager_DeclineGroupInvitation(string _groupId = "")
        {
            Console.WriteLine("DeclineGroupInvitation only can be called in OnInvitationReceivedFromGroup. ");
            Console.WriteLine($"Here only set to decline invitation for group id:{_groupId}");
            select_context.group_invitation_select[_groupId] = 0;
        }

        public void CallFunc_IGroupManager_DeclineGroupJoinApplication(string _groupId = "")
        {
            Console.WriteLine("DeclineGroupJoinApplication only can be called in OnRequestToJoinReceivedFromGroup. ");
            Console.WriteLine($"Here only set to accept application for group id:{_groupId}");
            select_context.group_application_select[_groupId] = 0;
        }

        public void CallFunc_IGroupManager_DestroyGroup(string _groupId = "")
        {
            string groupId = "";

            if (_groupId.Length > 0)
                groupId = _groupId;
            else
                groupId = GetParamValueFromContext(0);

            SDKClient.Instance.GroupManager.DestroyGroup(groupId, new CallBack(
                onSuccess: () =>
                {
                    Console.WriteLine($"DestroyGroup success.");
                },
                onError: (code, desc) =>
                {
                    Console.WriteLine($"DestroyGroup failed, code:{code}, desc:{desc}");
                }
            ));
        }

        public void CallFunc_IGroupManager_DownloadGroupSharedFile(string _groupId = "", string _fileId="", string _savePath="")
        {
            string groupId = "";
            string fileId = "";
            string savePath = "";

            if (_groupId.Length > 0)
                groupId = _groupId;
            else
                groupId = GetParamValueFromContext(0);

            if (_fileId.Length > 0)
                fileId = _fileId;
            else
                fileId = GetParamValueFromContext(1);

            if (_savePath.Length > 0)
                savePath = _savePath;
            else
                savePath = GetParamValueFromContext(2);

            SDKClient.Instance.GroupManager.DownloadGroupSharedFile(groupId, fileId, savePath, new CallBack(
                onSuccess: () =>
                {
                    Console.WriteLine($"DownloadGroupSharedFile success.");
                },
                onError: (code, desc) =>
                {
                    Console.WriteLine($"DownloadGroupSharedFile failed, code:{code}, desc:{desc}");
                }
            ));
        }

        public void CallFunc_IGroupManager_GetGroupAnnouncementFromServer(string _groupId = "")
        {
            string groupId = "";

            if (_groupId.Length > 0)
                groupId = _groupId;
            else
                groupId = GetParamValueFromContext(0);


            SDKClient.Instance.GroupManager.GetGroupAnnouncementFromServer(groupId, new ValueCallBack<string>(
                onSuccess: (str) =>
                {
                    Console.WriteLine($"GetGroupAnnouncementFromServer success, announcement:{str}");
                },
                onError: (code, desc) =>
                {
                    Console.WriteLine($"GetGroupAnnouncementFromServer failed, code:{code}, desc:{desc}");
                }
            ));
        }

        public void CallFunc_IGroupManager_GetGroupBlockListFromServer(string _groupId = "", int _num=-1, int _size=-1)
        {
            string groupId = "";
            int num = -1;
            int size = -1;

            if (_groupId.Length > 0)
                groupId = _groupId;
            else
                groupId = GetParamValueFromContext(0);

            if (-1 != _num)
                num = _num;
            else
                num = GetIntFromString(GetParamValueFromContext(1));

            if (-1 != _size)
                size = _size;
            else
                size = GetIntFromString(GetParamValueFromContext(2));

            SDKClient.Instance.GroupManager.GetGroupBlockListFromServer(groupId, num, size, new ValueCallBack<List<string>>(
                onSuccess: (list) =>
                {
                    Console.WriteLine($"GetGroupBlockListFromServer, block list num:{list.Count}");
                    foreach (var it in list)
                    {
                        Console.WriteLine($"block item:{it}");
                    }
                },
                onError: (code, desc) =>
                {
                    Console.WriteLine($"GetGroupBlockListFromServer failed, code:{code}, desc:{desc}");
                }
            ));
        }

        public void CallFunc_IGroupManager_GetGroupFileListFromServer(string _groupId = "", int _num = -1, int _size = -1)
        {
            string groupId = "";
            int num = -1;
            int size = -1;

            if (_groupId.Length > 0)
                groupId = _groupId;
            else
                groupId = GetParamValueFromContext(0);

            if (-1 != _num)
                num = _num;
            else
                num = GetIntFromString(GetParamValueFromContext(1));

            if (-1 != _size)
                size = _size;
            else
                size = GetIntFromString(GetParamValueFromContext(2));

            SDKClient.Instance.GroupManager.GetGroupFileListFromServer(groupId, num, size, new ValueCallBack<List<GroupSharedFile>>(
                onSuccess: (list) =>
                {
                    Console.WriteLine($"GetGroupFileListFromServer, file list num:{list.Count}");
                    foreach (var it in list)
                    {
                        Console.WriteLine($"file item: fid:{it.FileId}, fn:{it.FileName}");
                    }
                },
                onError: (code, desc) =>
                {
                    Console.WriteLine($"GetGroupFileListFromServer failed, code:{code}, desc:{desc}");
                }
            ));
        }

        public void CallFunc_IGroupManager_GetGroupMemberListFromServer(string _groupId = "", int _size = -1, string _cursor="")
        {
            string groupId = "";
            int size = -1;
            string cursor = "";

            if (_groupId.Length > 0)
                groupId = _groupId;
            else
                groupId = GetParamValueFromContext(0);

            if (-1 != _size)
                size = _size;
            else
                size = GetIntFromString(GetParamValueFromContext(1));

            if (_cursor.Length > 0)
                cursor = _cursor;
            else
                cursor = GetParamValueFromContext(2);

            SDKClient.Instance.GroupManager.GetGroupMemberListFromServer(groupId, size, cursor, new ValueCallBack<CursorResult<string>>(
                onSuccess: (result) =>
                {
                    Console.WriteLine($"GetGroupMemberListFromServer, member list num:{result.Data.Count}, cursor:{result.Cursor}");
                    foreach (var it in result.Data)
                    {
                        Console.WriteLine($"member : {it}");
                    }
                },
                onError: (code, desc) =>
                {
                    Console.WriteLine($"GetGroupMemberListFromServer failed, code:{code}, desc:{desc}");
                }
            ));
        }

        public void CallFunc_IGroupManager_GetGroupMuteListFromServer(string _groupId = "", int _num = -1, int _size = -1)
        {
            string groupId = "";
            int num = -1;
            int size = -1;

            if (_groupId.Length > 0)
                groupId = _groupId;
            else
                groupId = GetParamValueFromContext(0);

            if (-1 != _num)
                num = _num;
            else
                num = GetIntFromString(GetParamValueFromContext(1));

            if (-1 != _size)
                size = _size;
            else
                size = GetIntFromString(GetParamValueFromContext(2));

            SDKClient.Instance.GroupManager.GetGroupMuteListFromServer(groupId, num, size, new ValueCallBack<List<string>> (
                onSuccess: (list) =>
                {
                    Console.WriteLine($"GetGroupMuteListFromServer, mute list num:{list.Count}");
                    foreach (var it in list)
                    {
                        Console.WriteLine($"mute item: fid:{it}");
                    }
                },
                onError: (code, desc) =>
                {
                    Console.WriteLine($"GetGroupMuteListFromServer failed, code:{code}, desc:{desc}");
                }
            ));
        }

        public void CallFunc_IGroupManager_GetGroupSpecificationFromServer(string _groupId = "")
        {
            string groupId = "";

            if (_groupId.Length > 0)
                groupId = _groupId;
            else
                groupId = GetParamValueFromContext(0);

            SDKClient.Instance.GroupManager.GetGroupSpecificationFromServer(groupId, new ValueCallBack<Group>(
                onSuccess: (group) =>
                {
                    Console.WriteLine($"GetGroupSpecificationFromServer sucess ===============");
                    Console.WriteLine($"groupid: {group.GroupId}");
                    Console.WriteLine($"Name: {group.Name}");
                    Console.WriteLine($"Description: {group.Description}");
                    Console.WriteLine($"Owner: {group.Owner}");
                    Console.WriteLine($"Annoumcement: {group.Annoumcement}");
                    Console.WriteLine($"MemberCount: {group.MemberCount}");
                    string members = string.Join(",", group.MemberList.ToArray());
                    string admins = string.Join(",", group.AdminList.ToArray());
                    string blocks = string.Join(",", group.BlockList.ToArray());
                    //string mutes = string.Join(",", group.MuteList.ToArray());
                    Console.WriteLine($"MemberList: {members}");
                    Console.WriteLine($"AdminList: {admins}");
                    Console.WriteLine($"BlockList: {blocks}");
                    //Console.WriteLine($"MuteList: {mutes}");
                    Console.WriteLine($"NoticeEnabled: {group.NoticeEnabled}");
                    Console.WriteLine($"MessageBlocked: {group.MessageBlocked}");
                    Console.WriteLine($"IsAllMemberMuted: {group.IsAllMemberMuted}");
                    Console.WriteLine($"option style: {group.Options.Style}");
                    Console.WriteLine($"option MaxCount: {group.Options.MaxCount}");
                    Console.WriteLine($"option InviteNeedConfirm: {group.Options.InviteNeedConfirm}");
                    Console.WriteLine($"option Ext: {group.Options.Ext}");
                    Console.WriteLine($"=======================================================");
                },
                onError: (code, desc) =>
                {
                    Console.WriteLine($"GetGroupSpecificationFromServer failed, code:{code}, desc:{desc}");
                }
            ));
        }

        public void CallFunc_IGroupManager_GetGroupWhiteListFromServer(string _groupId = "")
        {
            string groupId = "";

            if (_groupId.Length > 0)
                groupId = _groupId;
            else
                groupId = GetParamValueFromContext(0);

            SDKClient.Instance.GroupManager.GetGroupWhiteListFromServer(groupId, new ValueCallBack<List<string>>(
                onSuccess: (list) =>
                {
                    Console.WriteLine($"GetGroupWhiteListFromServer, white list num:{list.Count}");
                    foreach (var it in list)
                    {
                        Console.WriteLine($"white item: {it}");
                    }
                },
                onError: (code, desc) =>
                {
                    Console.WriteLine($"GetGroupWhiteListFromServer failed, code:{code}, desc:{desc}");
                }
            ));
        }

        public void CallFunc_IGroupManager_GetGroupWithId(string _groupId = "")
        {
            string groupId = "";

            if (_groupId.Length > 0)
                groupId = _groupId;
            else
                groupId = GetParamValueFromContext(0);

            Group group = SDKClient.Instance.GroupManager.GetGroupWithId(groupId);
            if(null == group)
            {
                Console.WriteLine($"GetGroupWithId failed, null group");
            }
            else
            {
                Console.WriteLine($"GetGroupWithId sucess ============================");
                Console.WriteLine($"groupid: {group.GroupId}");
                Console.WriteLine($"Name: {group.Name}");
                Console.WriteLine($"Description: {group.Description}");
                Console.WriteLine($"Owner: {group.Owner}");
                Console.WriteLine($"Annoumcement: {group.Annoumcement}");
                Console.WriteLine($"MemberCount: {group.MemberCount}");
                string members = string.Join(",", group.MemberList.ToArray());
                string admins = string.Join(",", group.AdminList.ToArray());
                string blocks = string.Join(",", group.BlockList.ToArray());
                //string mutes = string.Join(",", group.MuteList.ToArray());
                Console.WriteLine($"MemberList: {members}");
                Console.WriteLine($"AdminList: {admins}");
                Console.WriteLine($"BlockList: {blocks}");
                //Console.WriteLine($"MuteList: {mutes}");
                Console.WriteLine($"NoticeEnabled: {group.NoticeEnabled}");
                Console.WriteLine($"MessageBlocked: {group.MessageBlocked}");
                Console.WriteLine($"IsAllMemberMuted: {group.IsAllMemberMuted}");
                Console.WriteLine($"option style: {group.Options.Style}");
                Console.WriteLine($"option MaxCount: {group.Options.MaxCount}");
                Console.WriteLine($"option InviteNeedConfirm: {group.Options.InviteNeedConfirm}");
                Console.WriteLine($"option Ext: {group.Options.Ext}");
                Console.WriteLine($"=======================================================");
            }
        }

        public void CallFunc_IGroupManager_GetJoinedGroups()
        {
            List<Group> groupList = SDKClient.Instance.GroupManager.GetJoinedGroups();
            if (groupList.Count == 0)
            {
                Console.WriteLine($"GetJoinedGroups not find any groups");
            }
            else
            {
                int i = 1;
                foreach(var group in groupList)
                {
                    Console.WriteLine($"GetJoinedGroups sucess ============================{i}");
                    Console.WriteLine($"groupid: {group.GroupId}");
                    Console.WriteLine($"Name: {group.Name}");
                    Console.WriteLine($"Description: {group.Description}");
                    Console.WriteLine($"Owner: {group.Owner}");
                    Console.WriteLine($"Annoumcement: {group.Annoumcement}");
                    Console.WriteLine($"MemberCount: {group.MemberCount}");
                    string members = string.Join(",", group.MemberList.ToArray());
                    string admins = string.Join(",", group.AdminList.ToArray());
                    string blocks = string.Join(",", group.BlockList.ToArray());
                    //string mutes = string.Join(",", group.MuteList.ToArray());
                    Console.WriteLine($"MemberList: {members}");
                    Console.WriteLine($"AdminList: {admins}");
                    Console.WriteLine($"BlockList: {blocks}");
                    //Console.WriteLine($"MuteList: {mutes}");
                    Console.WriteLine($"NoticeEnabled: {group.NoticeEnabled}");
                    Console.WriteLine($"MessageBlocked: {group.MessageBlocked}");
                    Console.WriteLine($"IsAllMemberMuted: {group.IsAllMemberMuted}");
                    Console.WriteLine($"option style: {group.Options.Style}");
                    Console.WriteLine($"option MaxCount: {group.Options.MaxCount}");
                    Console.WriteLine($"option InviteNeedConfirm: {group.Options.InviteNeedConfirm}");
                    Console.WriteLine($"option Ext: {group.Options.Ext}");
                    Console.WriteLine($"=======================================================");
                    i++;
                }
            }
        }

        public void CallFunc_IGroupManager_FetchJoinedGroupsFromServer(int _num=-1, int _size=-1)
        {
            int num = -1;
            int size = -1;

            if (-1 != _num)
                num = _num;
            else
                num = GetIntFromString(GetParamValueFromContext(0));

            if (-1 != _size)
                size = _size;
            else
                size = GetIntFromString(GetParamValueFromContext(1));

            SDKClient.Instance.GroupManager.FetchJoinedGroupsFromServer(num, size, handle: new ValueCallBack<List<Group>>(
                onSuccess: (groupList) => {
                    int i = 1;
                    foreach (var group in groupList)
                    {
                        Console.WriteLine($"FetchJoinedGroupsFromServer sucess ====================={i}");
                        Console.WriteLine($"groupid: {group.GroupId}");
                        Console.WriteLine($"Name: {group.Name}");
                        Console.WriteLine($"Description: {group.Description}");
                        Console.WriteLine($"Owner: {group.Owner}");
                        Console.WriteLine($"Annoumcement: {group.Annoumcement}");
                        Console.WriteLine($"MemberCount: {group.MemberCount}");
                        string members = string.Join(",", group.MemberList.ToArray());
                        string admins = string.Join(",", group.AdminList.ToArray());
                        string blocks = string.Join(",", group.BlockList.ToArray());
                        //string mutes = string.Join(",", group.MuteList.ToArray());
                        Console.WriteLine($"MemberList: {members}");
                        Console.WriteLine($"AdminList: {admins}");
                        Console.WriteLine($"BlockList: {blocks}");
                        //Console.WriteLine($"MuteList: {mutes}");
                        Console.WriteLine($"NoticeEnabled: {group.NoticeEnabled}");
                        Console.WriteLine($"MessageBlocked: {group.MessageBlocked}");
                        Console.WriteLine($"IsAllMemberMuted: {group.IsAllMemberMuted}");
                        Console.WriteLine($"option style: {group.Options.Style}");
                        Console.WriteLine($"option MaxCount: {group.Options.MaxCount}");
                        Console.WriteLine($"option InviteNeedConfirm: {group.Options.InviteNeedConfirm}");
                        Console.WriteLine($"option Ext: {group.Options.Ext}");
                        Console.WriteLine($"=======================================================");
                        i++;
                    }
                },
                onError: (code, desc) =>
                {
                    Console.WriteLine($"FetchJoinedGroupsFromServer failed, code:{code}, desc:{desc}");
                }
            ));
        }

        public void CallFunc_IGroupManager_FetchPublicGroupsFromServer(int _size = -1, string _cursor = "")
        {
            int size = -1;
            string cursor = "";

            if (-1 != _size)
                size = _size;
            else
                size = GetIntFromString(GetParamValueFromContext(0));

            if (_cursor.Length > 0)
                cursor = _cursor;
            else
                cursor = GetParamValueFromContext(1);

            SDKClient.Instance.GroupManager.FetchPublicGroupsFromServer(size, cursor, handle: new ValueCallBack<CursorResult<GroupInfo>>(
                onSuccess: (result) => {
                    Console.WriteLine($"FetchPublicGroupsFromServer, public group num:{result.Data.Count}, cursor:{result.Cursor}");
                    foreach (var it in result.Data)
                    {
                        /*
                        string id = Encoding.UTF8.GetString(Encoding.Unicode.GetBytes(it.GroupId));
                        int index = id.IndexOf('\0');
                        if (index > 0)
                            id = id.Substring(0,index);
                        
                        string name = Encoding.UTF8.GetString(Encoding.Unicode.GetBytes(it.GroupName));
                        int index = -1;
                        index = name.IndexOf('\0');
                        if (index > 0)
                            name = name.Substring(0, index);
                        */
                        /*
                        string name = Encoding.UTF8.GetString(Encoding.UTF8.GetBytes(it.GroupName));
                        int index = -1;
                        index = name.IndexOf('\0');
                        if (index > 0)
                            name = name.Substring(0, index);
                        */
                        Console.WriteLine($"group id : {it.GroupId}; name: {it.GroupName}");
                    }
                },
                onError: (code, desc) =>
                {
                    Console.WriteLine($"FetchPublicGroupsFromServer failed, code:{code}, desc:{desc}");
                }
            ));
        }

        public void CallFunc_IGroupManager_JoinPublicGroup(string _groupId = "")
        {
            string groupId = "";

            if (_groupId.Length > 0)
                groupId = _groupId;
            else
                groupId = GetParamValueFromContext(0);

            SDKClient.Instance.GroupManager.JoinPublicGroup(groupId, handle: new CallBack(
                onSuccess: () => {
                    Console.WriteLine($"JoinPublicGroup success");
                },
                onError: (code, desc) =>
                {
                    Console.WriteLine($"JoinPublicGroup failed, code:{code}, desc:{desc}");
                }
            ));
        }

        public void CallFunc_IGroupManager_LeaveGroup(string _groupId = "")
        {
            string groupId = "";

            if (_groupId.Length > 0)
                groupId = _groupId;
            else
                groupId = GetParamValueFromContext(0);

            SDKClient.Instance.GroupManager.LeaveGroup(groupId, handle: new CallBack(
                onSuccess: () => {
                    Console.WriteLine($"LeaveGroup success");
                },
                onError: (code, desc) =>
                {
                    Console.WriteLine($"LeaveGroup failed, code:{code}, desc:{desc}");
                }
            ));
        }

        public void CallFunc_IGroupManager_MuteGroupAllMembers(string _groupId = "")
        {
            string groupId = "";

            if (_groupId.Length > 0)
                groupId = _groupId;
            else
                groupId = GetParamValueFromContext(0);

            SDKClient.Instance.GroupManager.MuteGroupAllMembers(groupId, handle: new CallBack(
                onSuccess: () => {
                    Console.WriteLine($"MuteGroupAllMembers success");
                },
                onError: (code, desc) =>
                {
                    Console.WriteLine($"MuteGroupAllMembers failed, code:{code}, desc:{desc}");
                }
            ));
        }

        public void CallFunc_IGroupManager_MuteGroupMembers(string _groupId = "", string _member1 = "", string _member2 = "")
        {
            string groupId = "";
            string member1 = "";
            string member2 = "";

            if (_groupId.Length > 0)
                groupId = _groupId;
            else
                groupId = GetParamValueFromContext(0);

            if (_member1.Length > 0)
                member1 = _member1;
            else
                member1 = GetParamValueFromContext(1);

            if (_member2.Length > 0)
                member2 = _member2;
            else
                member2 = GetParamValueFromContext(2);

            List<string> members = new List<string>();
            members.Add(member1);
            members.Add(member2);

            SDKClient.Instance.GroupManager.MuteGroupMembers(groupId, members, handle: new CallBack(
                onSuccess: () => {
                    Console.WriteLine($"MuteGroupMembers success");
                },
                onError: (code, desc) =>
                {
                    Console.WriteLine($"MuteGroupMembers failed, code:{code}, desc:{desc}");
                }
            ));
        }

        public void CallFunc_IGroupManager_RemoveGroupAdmin(string _groupId = "", string _memberId = "")
        {
            string groupId = "";
            string memberId = "";

            if (_groupId.Length > 0)
                groupId = _groupId;
            else
                groupId = GetParamValueFromContext(0);

            if (_memberId.Length > 0)
                memberId = _memberId;
            else
                memberId = GetParamValueFromContext(1);

            SDKClient.Instance.GroupManager.RemoveGroupAdmin(groupId, memberId, handle: new CallBack(
                onSuccess: () => {
                    Console.WriteLine($"RemoveGroupAdmin success");
                },
                onError: (code, desc) =>
                {
                    Console.WriteLine($"RemoveGroupAdmin failed, code:{code}, desc:{desc}");
                }
            ));
        }

        public void CallFunc_IGroupManager_DeleteGroupSharedFile(string _groupId = "", string _fileId = "")
        {
            string groupId = "";
            string fileId = "";

            if (_groupId.Length > 0)
                groupId = _groupId;
            else
                groupId = GetParamValueFromContext(0);

            if (_fileId.Length > 0)
                fileId = _fileId;
            else
                fileId = GetParamValueFromContext(1);

            SDKClient.Instance.GroupManager.DeleteGroupSharedFile(groupId, fileId, handle: new CallBack(
                onSuccess: () => {
                    Console.WriteLine($"DeleteGroupSharedFile success");
                },
                onError: (code, desc) =>
                {
                    Console.WriteLine($"DeleteGroupSharedFile failed, code:{code}, desc:{desc}");
                }
            ));
        }

        public void CallFunc_IGroupManager_DeleteGroupMembers(string _groupId = "", string _member1 = "", string _member2 = "")
        {
            string groupId = "";
            string member1 = "";
            string member2 = "";

            if (_groupId.Length > 0)
                groupId = _groupId;
            else
                groupId = GetParamValueFromContext(0);

            if (_member1.Length > 0)
                member1 = _member1;
            else
                member1 = GetParamValueFromContext(1);

            if (_member2.Length > 0)
                member2 = _member2;
            else
                member2 = GetParamValueFromContext(2);

            List<string> members = new List<string>();
            members.Add(member1);
            members.Add(member2);

            SDKClient.Instance.GroupManager.DeleteGroupMembers(groupId, members, handle: new CallBack(
                onSuccess: () => {
                    Console.WriteLine($"DeleteGroupMembers success");
                },
                onError: (code, desc) =>
                {
                    Console.WriteLine($"DeleteGroupMembers failed, code:{code}, desc:{desc}");
                }
            ));
        }

        public void CallFunc_IGroupManager_RemoveGroupWhiteList(string _groupId = "", string _member1 = "", string _member2 = "")
        {
            string groupId = "";
            string member1 = "";
            string member2 = "";

            if (_groupId.Length > 0)
                groupId = _groupId;
            else
                groupId = GetParamValueFromContext(0);

            if (_member1.Length > 0)
                member1 = _member1;
            else
                member1 = GetParamValueFromContext(1);

            if (_member2.Length > 0)
                member2 = _member2;
            else
                member2 = GetParamValueFromContext(2);

            List<string> members = new List<string>();
            members.Add(member1);
            members.Add(member2);

            SDKClient.Instance.GroupManager.RemoveGroupWhiteList(groupId, members, handle: new CallBack(
                onSuccess: () => {
                    Console.WriteLine($"RemoveGroupWhiteList success");
                },
                onError: (code, desc) =>
                {
                    Console.WriteLine($"RemoveGroupWhiteList failed, code:{code}, desc:{desc}");
                }
            ));
        }

        public void CallFunc_IGroupManager_UnBlockGroup(string _groupId = "")
        {
            string groupId = "";

            if (_groupId.Length > 0)
                groupId = _groupId;
            else
                groupId = GetParamValueFromContext(0);

            SDKClient.Instance.GroupManager.UnBlockGroup(groupId, handle: new CallBack(
                onSuccess: () => {
                    Console.WriteLine($"UnBlockGroup success");
                },
                onError: (code, desc) =>
                {
                    Console.WriteLine($"UnBlockGroup failed, code:{code}, desc:{desc}");
                }
            ));
        }

        public void CallFunc_IGroupManager_UnBlockGroupMembers(string _groupId = "", string _memberId1 = "", string _memberId2 = "", string _memberId3 = "")
        {
            string groupId = "";
            string memberId1 = "";
            string memberId2 = "";
            string memberId3 = "";

            if (_groupId.Length > 0)
                groupId = _groupId;
            else
                groupId = GetParamValueFromContext(0);

            if (_memberId1.Length > 0)
                memberId1 = _memberId1;
            else
                memberId1 = GetParamValueFromContext(1);

            if (_memberId2.Length > 0)
                memberId2 = _memberId2;
            else
                memberId2 = GetParamValueFromContext(2);

            if (_memberId3.Length > 0)
                memberId3 = _memberId3;
            else
                memberId2 = GetParamValueFromContext(3);

            List<string> members = new List<string>();
            members.Add(memberId1);
            members.Add(memberId2);
            members.Add(memberId3);

            SDKClient.Instance.GroupManager.UnBlockGroupMembers(groupId, members, new CallBack(
                onSuccess: () =>
                {
                    Console.WriteLine($"UnBlockGroupMembers success.");
                },
                onError: (code, desc) =>
                {
                    Console.WriteLine($"UnBlockGroupMembers failed, code:{code}, desc:{desc}");
                }
            ));
        }

        public void CallFunc_IGroupManager_UnMuteGroupAllMembers(string _groupId = "")
        {
            string groupId = "";

            if (_groupId.Length > 0)
                groupId = _groupId;
            else
                groupId = GetParamValueFromContext(0);

            SDKClient.Instance.GroupManager.UnMuteGroupAllMembers(groupId, handle: new CallBack(
                onSuccess: () => {
                    Console.WriteLine($"UnMuteGroupAllMembers success");
                },
                onError: (code, desc) =>
                {
                    Console.WriteLine($"UnMuteGroupAllMembers failed, code:{code}, desc:{desc}");
                }
            ));
        }

        public void CallFunc_IGroupManager_UnMuteGroupMembers(string _groupId = "", string _memberId1 = "", string _memberId2 = "", string _memberId3 = "")
        {
            string groupId = "";
            string memberId1 = "";
            string memberId2 = "";
            string memberId3 = "";

            if (_groupId.Length > 0)
                groupId = _groupId;
            else
                groupId = GetParamValueFromContext(0);

            if (_memberId1.Length > 0)
                memberId1 = _memberId1;
            else
                memberId1 = GetParamValueFromContext(1);

            if (_memberId2.Length > 0)
                memberId2 = _memberId2;
            else
                memberId2 = GetParamValueFromContext(2);

            if (_memberId3.Length > 0)
                memberId3 = _memberId3;
            else
                memberId2 = GetParamValueFromContext(3);

            List<string> members = new List<string>();
            members.Add(memberId1);
            members.Add(memberId2);
            members.Add(memberId3);

            SDKClient.Instance.GroupManager.UnMuteGroupMembers(groupId, members, new CallBack(
                onSuccess: () =>
                {
                    Console.WriteLine($"UnMuteGroupMembers success.");
                },
                onError: (code, desc) =>
                {
                    Console.WriteLine($"UnMuteGroupMembers failed, code:{code}, desc:{desc}");
                }
            ));
        }

        public void CallFunc_IGroupManager_UpdateGroupAnnouncement(string _groupId = "", string _announcement = "")
        {
            string groupId = "";
            string announcement = "";

            if (_groupId.Length > 0)
                groupId = _groupId;
            else
                groupId = GetParamValueFromContext(0);

            if (_announcement.Length > 0)
                announcement = _announcement;
            else
                announcement = GetParamValueFromContext(1);

            SDKClient.Instance.GroupManager.UpdateGroupAnnouncement(groupId, announcement, new CallBack(
                onSuccess: () =>
                {
                    Console.WriteLine($"UpdateGroupAnnouncement success.");
                },
                onError: (code, desc) =>
                {
                    Console.WriteLine($"UpdateGroupAnnouncement failed, code:{code}, desc:{desc}");
                }
            ));
        }

        public void CallFunc_IGroupManager_UpdateGroupExt(string _groupId = "", string _ext = "")
        {
            string groupId = "";
            string ext = "";

            if (_groupId.Length > 0)
                groupId = _groupId;
            else
                groupId = GetParamValueFromContext(0);

            if (_ext.Length > 0)
                ext = _ext;
            else
                ext = GetParamValueFromContext(1);

            SDKClient.Instance.GroupManager.UpdateGroupExt(groupId, ext, new CallBack(
                onSuccess: () =>
                {
                    Console.WriteLine($"UpdateGroupExt success.");
                },
                onError: (code, desc) =>
                {
                    Console.WriteLine($"UpdateGroupExt failed, code:{code}, desc:{desc}");
                }
            ));
        }

        public void CallFunc_IGroupManager_UploadGroupSharedFile(string _groupId = "", string _filePath = "")
        {
            string groupId = "";
            string filePath = "";

            if (_groupId.Length > 0)
                groupId = _groupId;
            else
                groupId = GetParamValueFromContext(0);

            if (_filePath.Length > 0)
                filePath = _filePath;
            else
                filePath = GetParamValueFromContext(1);

            SDKClient.Instance.GroupManager.UploadGroupSharedFile(groupId, filePath, new CallBack(
                onSuccess: () =>
                {
                    Console.WriteLine($"UploadGroupSharedFile success.");
                },
                onError: (code, desc) =>
                {
                    Console.WriteLine($"UploadGroupSharedFile failed, code:{code}, desc:{desc}");
                },
                onProgress: (progress) =>
                {
                    Console.WriteLine($"UploadGroupSharedFile process:{progress}");
                }
            ));
        }

        public void CallFunc_IGroupManager()
        {
            if (select_context.level2_item.CompareTo("applyJoinToGroup") == 0)
            {
                CallFunc_IGroupManager_applyJoinToGroup();
                return;
            }

            if (select_context.level2_item.CompareTo("AcceptGroupInvitation") == 0)
            {
                CallFunc_IGroupManager_AcceptGroupInvitation();
                return;
            }

            if (select_context.level2_item.CompareTo("AcceptGroupJoinApplication") == 0)
            {
                CallFunc_IGroupManager_AcceptGroupJoinApplication();
                return;
            }

            if (select_context.level2_item.CompareTo("AddGroupAdmin") == 0)
            {
                CallFunc_IGroupManager_AddGroupAdmin();
                return;
            }

            if (select_context.level2_item.CompareTo("AddGroupMembers") == 0)
            {
                CallFunc_IGroupManager_AddGroupMembers();
                return;
            }

            if (select_context.level2_item.CompareTo("AddGroupWhiteList") == 0)
            {
                CallFunc_IGroupManager_AddGroupWhiteList();
                return;
            }

            if (select_context.level2_item.CompareTo("BlockGroup") == 0)
            {
                CallFunc_IGroupManager_BlockGroup();
                return;
            }

            if (select_context.level2_item.CompareTo("BlockGroupMembers") == 0)
            {
                CallFunc_IGroupManager_BlockGroupMembers();
                return;
            }

            if (select_context.level2_item.CompareTo("ChangeGroupDescription") == 0)
            {
                CallFunc_IGroupManager_ChangeGroupDescription();
                return;
            }

            if (select_context.level2_item.CompareTo("ChangeGroupName") == 0)
            {
                CallFunc_IGroupManager_ChangeGroupName();
                return;
            }

            if (select_context.level2_item.CompareTo("ChangeGroupOwner") == 0)
            {
                CallFunc_IGroupManager_ChangeGroupOwner();
                return;
            }

            if (select_context.level2_item.CompareTo("CheckIfInGroupWhiteList") == 0)
            {
                CallFunc_IGroupManager_CheckIfInGroupWhiteList();
                return;
            }

            if (select_context.level2_item.CompareTo("CreateGroup") == 0)
            {
                CallFunc_IGroupManager_CreateGroup();
                return;
            }

            if (select_context.level2_item.CompareTo("DeclineGroupInvitation") == 0)
            {
                CallFunc_IGroupManager_DeclineGroupInvitation();
                return;
            }

            if (select_context.level2_item.CompareTo("DeclineGroupJoinApplication") == 0)
            {
                CallFunc_IGroupManager_DeclineGroupJoinApplication();
                return;
            }

            if (select_context.level2_item.CompareTo("DestroyGroup") == 0)
            {
                CallFunc_IGroupManager_DestroyGroup();
                return;
            }

            if (select_context.level2_item.CompareTo("DownloadGroupSharedFile") == 0)
            {
                CallFunc_IGroupManager_DownloadGroupSharedFile();
                return;
            }

            if (select_context.level2_item.CompareTo("GetGroupAnnouncementFromServer") == 0)
            {
                CallFunc_IGroupManager_GetGroupAnnouncementFromServer();
                return;
            }

            if (select_context.level2_item.CompareTo("GetGroupBlockListFromServer") == 0)
            {
                CallFunc_IGroupManager_GetGroupBlockListFromServer();
                return;
            }

            if (select_context.level2_item.CompareTo("GetGroupFileListFromServer") == 0)
            {
                CallFunc_IGroupManager_GetGroupFileListFromServer();
                return;
            }

            if (select_context.level2_item.CompareTo("GetGroupMemberListFromServer") == 0)
            {
                CallFunc_IGroupManager_GetGroupMemberListFromServer();
                return;
            }

            if (select_context.level2_item.CompareTo("GetGroupMuteListFromServer") == 0)
            {
                CallFunc_IGroupManager_GetGroupMuteListFromServer();
                return;
            }

            if (select_context.level2_item.CompareTo("GetGroupSpecificationFromServer") == 0)
            {
                CallFunc_IGroupManager_GetGroupSpecificationFromServer();
                return;
            }

            if (select_context.level2_item.CompareTo("GetGroupWhiteListFromServer") == 0)
            {
                CallFunc_IGroupManager_GetGroupWhiteListFromServer();
                return;
            }

            if (select_context.level2_item.CompareTo("GetGroupWithId") == 0)
            {
                CallFunc_IGroupManager_GetGroupWithId();
                return;
            }

            if (select_context.level2_item.CompareTo("GetJoinedGroups") == 0)
            {
                CallFunc_IGroupManager_GetJoinedGroups();
                return;
            }

            if (select_context.level2_item.CompareTo("FetchJoinedGroupsFromServer") == 0)
            {
                CallFunc_IGroupManager_FetchJoinedGroupsFromServer();
                return;
            }

            if (select_context.level2_item.CompareTo("FetchPublicGroupsFromServer") == 0)
            {
                CallFunc_IGroupManager_FetchPublicGroupsFromServer();
                return;
            }

            if (select_context.level2_item.CompareTo("JoinPublicGroup") == 0)
            {
                CallFunc_IGroupManager_JoinPublicGroup();
                return;
            }

            if (select_context.level2_item.CompareTo("LeaveGroup") == 0)
            {
                CallFunc_IGroupManager_LeaveGroup();
                return;
            }

            if (select_context.level2_item.CompareTo("MuteGroupAllMembers") == 0)
            {
                CallFunc_IGroupManager_MuteGroupAllMembers();
                return;
            }

            if (select_context.level2_item.CompareTo("MuteGroupMembers") == 0)
            {
                CallFunc_IGroupManager_MuteGroupMembers();
                return;
            }

            if (select_context.level2_item.CompareTo("RemoveGroupAdmin") == 0)
            {
                CallFunc_IGroupManager_RemoveGroupAdmin();
                return;
            }

            if (select_context.level2_item.CompareTo("DeleteGroupSharedFile") == 0)
            {
                CallFunc_IGroupManager_DeleteGroupSharedFile();
                return;
            }

            if (select_context.level2_item.CompareTo("DeleteGroupMembers") == 0)
            {
                CallFunc_IGroupManager_DeleteGroupMembers();
                return;
            }

            if (select_context.level2_item.CompareTo("RemoveGroupWhiteList") == 0)
            {
                CallFunc_IGroupManager_RemoveGroupWhiteList();
                return;
            }

            if (select_context.level2_item.CompareTo("UnBlockGroup") == 0)
            {
                CallFunc_IGroupManager_UnBlockGroup();
                return;
            }

            if (select_context.level2_item.CompareTo("UnBlockGroupMembers") == 0)
            {
                CallFunc_IGroupManager_UnBlockGroupMembers();
                return;
            }

            if (select_context.level2_item.CompareTo("UnMuteGroupAllMembers") == 0)
            {
                CallFunc_IGroupManager_UnMuteGroupAllMembers();
                return;
            }

            if (select_context.level2_item.CompareTo("UnMuteGroupMembers") == 0)
            {
                CallFunc_IGroupManager_UnMuteGroupMembers();
                return;
            }

            if (select_context.level2_item.CompareTo("UpdateGroupAnnouncement") == 0)
            {
                CallFunc_IGroupManager_UpdateGroupAnnouncement();
                return;
            }

            if (select_context.level2_item.CompareTo("UpdateGroupExt") == 0)
            {
                CallFunc_IGroupManager_UpdateGroupExt();
                return;
            }

            if (select_context.level2_item.CompareTo("UploadGroupSharedFile") == 0)
            {
                CallFunc_IGroupManager_UploadGroupSharedFile();
                return;
            }
        }

        public void CallFunc_IPushManager_GetNoDisturbGroups()
        {
            List<string> list = SDKClient.Instance.PushManager.GetNoDisturbGroups();
            if (list.Count > 0)
            {
                Console.WriteLine($"GetNoDisturbGroups completed, list has {list.Count} item.");
                foreach(var it in list)
                {
                    Console.WriteLine($"NoDisturbGroup item:{it}");
                }
            }
            else
            {
                Console.WriteLine($"GetNoDisturbGroups done, list is empty");
            }

        }

        public void CallFunc_IPushManager_GetPushConfig()
        {
            PushConfig config = SDKClient.Instance.PushManager.GetPushConfig();
            if (null != config)
            {
                Console.WriteLine($"GetPushConfig completed.");
                Console.WriteLine($"NoDisturb:{config.NoDisturb}, NoDStart:{config.NoDisturbStartHour}, NoDEnd:{config.NoDisturbEndHour}, stype:{config.Style}");
            }
            else
            {
                Console.WriteLine("GetPushConfig done, PushConfig is null.");
            }
        }

        public void CallFunc_IPushManager_GetPushConfigFromServer()
        {
            SDKClient.Instance.PushManager.GetPushConfigFromServer(new ValueCallBack<PushConfig>(
                onSuccess: (config) => {
                    if (null != config)
                    {
                        Console.WriteLine($"GetPushConfigFromServer completed.");
                        Console.WriteLine($"NoDisturb:{config.NoDisturb}, NoDStart:{config.NoDisturbStartHour}, NoDEnd:{config.NoDisturbEndHour}, stype:{config.Style}");
                    }
                    else
                    {
                        Console.WriteLine("GetPushConfigFromServer done, PushConfig is null.");
                    }
                },
                 onError: (code, desc) => {
                     Console.WriteLine($"GetPushConfigFromServer failed, code:{code}, desc:{desc}");
                 }
            ));
        }

        public void CallFunc_IPushManager_UpdatePushNickName()
        {
            string nk = GetParamValueFromContext(0);
            SDKClient.Instance.PushManager.UpdatePushNickName(nk, new CallBack(
                onSuccess: () => {
                    Console.WriteLine($"UpdatePushNickName success.");
                },
                onError: (code, desc) => {
                    Console.WriteLine($"UpdatePushNickName failed, code:{code}, desc:{desc}");
                }
            ));
        }

        public void CallFunc_IPushManager_UpdateHMSPushToken()
        {
            string nk = GetParamValueFromContext(0);
            SDKClient.Instance.PushManager.UpdateHMSPushToken(nk, new CallBack(
                onSuccess: () => {
                    Console.WriteLine($"UpdateHMSPushToken success.");
                },
                onError: (code, desc) => {
                    Console.WriteLine($"UpdateHMSPushToken failed, code:{code}, desc:{desc}");
                }
            ));
        }

        public void CallFunc_IPushManager_UpdateFCMPushToken()
        {
            string nk = GetParamValueFromContext(0);
            SDKClient.Instance.PushManager.UpdateFCMPushToken(nk, new CallBack(
                onSuccess: () => {
                    Console.WriteLine($"UpdateFCMPushToken success.");
                },
                onError: (code, desc) => {
                    Console.WriteLine($"UpdateFCMPushToken failed, code:{code}, desc:{desc}");
                }
            ));
        }

        public void CallFunc_IPushManager_UpdateAPNSPushToken()
        {
            string nk = GetParamValueFromContext(0);
            SDKClient.Instance.PushManager.UpdateAPNSPushToken(nk, new CallBack(
                onSuccess: () => {
                    Console.WriteLine($"UpdateAPNSPushToken success.");
                },
                onError: (code, desc) => {
                    Console.WriteLine($"UpdateAPNSPushToken failed, code:{code}, desc:{desc}");
                }
            ));
        }

        public void CallFunc_IPushManager_SetNoDisturb()
        {
            int noDisturb = GetIntFromString(GetParamValueFromContext(0));
            int startTime = GetIntFromString(GetParamValueFromContext(1));
            int endTime = GetIntFromString(GetParamValueFromContext(2));

            SDKClient.Instance.PushManager.SetNoDisturb(noDisturb == 0 ? false : true, startTime, endTime, new CallBack(
                onSuccess: () => {
                    Console.WriteLine($"SetNoDisturb success.");
                },
                onError: (code, desc) => {
                    Console.WriteLine($"SetNoDisturb failed, code:{code}, desc:{desc}");
                }
            ));
        }

        public void CallFunc_IPushManager_SetPushStyle()
        {
            int pushStyle = GetIntFromString(GetParamValueFromContext(0));
            SDKClient.Instance.PushManager.SetPushStyle(pushStyle == 0 ? PushStyle.Simple : PushStyle.Summary, new CallBack(
                onSuccess: () => {
                    Console.WriteLine($"SetPushStyle success.");
                },
                onError: (code, desc) => {
                    Console.WriteLine($"SetPushStyle failed, code:{code}, desc:{desc}");
                }
            ));
        }

        public void CallFunc_IPushManager_SetGroupToDisturb()
        {
            string groupId = GetParamValueFromContext(0);
            int noDisturb = GetIntFromString(GetParamValueFromContext(1));
            SDKClient.Instance.PushManager.SetGroupToDisturb(groupId, noDisturb == 0 ? false : true, new CallBack(
                onSuccess: () => {
                    Console.WriteLine($"SetGroupToDisturb success.");
                },
                onError: (code, desc) => {
                    Console.WriteLine($"SetGroupToDisturb failed, code:{code}, desc:{desc}");
                }
            ));
        }

        public void CallFunc_IPushManager()
        {
            if (select_context.level2_item.CompareTo("GetNoDisturbGroups") == 0)
            {
                CallFunc_IPushManager_GetNoDisturbGroups();
                return;
            }

            if (select_context.level2_item.CompareTo("GetPushConfig") == 0)
            {
                CallFunc_IPushManager_GetPushConfig();
                return;
            }

            if (select_context.level2_item.CompareTo("GetPushConfigFromServer") == 0)
            {
                CallFunc_IPushManager_GetPushConfigFromServer();
                return;
            }

            if (select_context.level2_item.CompareTo("UpdatePushNickName") == 0)
            {
                CallFunc_IPushManager_UpdatePushNickName();
                return;
            }

            if (select_context.level2_item.CompareTo("UpdateHMSPushToken") == 0)
            {
                CallFunc_IPushManager_UpdateHMSPushToken();
                return;
            }

            if (select_context.level2_item.CompareTo("UpdateFCMPushToken") == 0)
            {
                CallFunc_IPushManager_UpdateFCMPushToken();
                return;
            }

            if (select_context.level2_item.CompareTo("UpdateAPNSPushToken") == 0)
            {
                CallFunc_IPushManager_UpdateAPNSPushToken();
                return;
            }

            if (select_context.level2_item.CompareTo("SetNoDisturb") == 0)
            {
                CallFunc_IPushManager_SetNoDisturb();
                return;
            }

            if (select_context.level2_item.CompareTo("SetPushStyle") == 0)
            {
                CallFunc_IPushManager_SetPushStyle();
                return;
            }

            if (select_context.level2_item.CompareTo("SetGroupToDisturb") == 0)
            {
                CallFunc_IPushManager_SetGroupToDisturb();
                return;
            }
        }

        public void CallFunc_IRoomManager_AddRoomAdmin()
        {
            string roomId = GetParamValueFromContext(0);
            string memberId = GetParamValueFromContext(1);
            SDKClient.Instance.RoomManager.AddRoomAdmin(roomId, memberId, new CallBack(
                onSuccess: () => {
                    Console.WriteLine($"AddRoomAdmin success.");
                },
                onError: (code, desc) => {
                    Console.WriteLine($"AddRoomAdmin failed, code:{code}, desc:{desc}");
                }
            ));
        }

        public void CallFunc_IRoomManager_BlockRoomMembers()
        {
            string roomId = GetParamValueFromContext(0);
            string memberId1 = GetParamValueFromContext(1);
            string memberId2 = GetParamValueFromContext(2);
            List<string> list = new List<string>();
            list.Add(memberId1);
            list.Add(memberId2);
            SDKClient.Instance.RoomManager.BlockRoomMembers(roomId, list, new CallBack(
                onSuccess: () => {
                    Console.WriteLine($"BlockRoomMembers success.");
                },
                onError: (code, desc) => {
                    Console.WriteLine($"BlockRoomMembers failed, code:{code}, desc:{desc}");
                }
            ));
        }

        public void CallFunc_IRoomManager_ChangeRoomOwner()
        {
            string roomId = GetParamValueFromContext(0);
            string newOwner = GetParamValueFromContext(1);
            SDKClient.Instance.RoomManager.ChangeRoomOwner(roomId, newOwner, new CallBack(
                onSuccess: () => {
                    Console.WriteLine($"ChangeRoomOwner success.");
                },
                onError: (code, desc) => {
                    Console.WriteLine($"ChangeRoomOwner failed, code:{code}, desc:{desc}");
                }
            ));
        }

        public void CallFunc_IRoomManager_ChangeRoomDescription()
        {
            string roomId = GetParamValueFromContext(0);
            string newDesc = GetParamValueFromContext(1);
            SDKClient.Instance.RoomManager.ChangeRoomDescription(roomId, newDesc, new CallBack(
                onSuccess: () => {
                    Console.WriteLine($"ChangeRoomDescription success.");
                },
                onError: (code, desc) => {
                    Console.WriteLine($"ChangeRoomDescription failed, code:{code}, desc:{desc}");
                }
            ));
        }

        public void CallFunc_IRoomManager_ChangeRoomName()
        {
            string roomId = GetParamValueFromContext(0);
            string name = GetParamValueFromContext(1);
            SDKClient.Instance.RoomManager.ChangeRoomName(roomId, name, new CallBack(
                onSuccess: () => {
                    Console.WriteLine($"ChangeRoomName success.");
                },
                onError: (code, desc) => {
                    Console.WriteLine($"ChangeRoomName failed, code:{code}, desc:{desc}");
                }
            ));
        }

        public void CallFunc_IRoomManager_CreateRoom()
        {
            string name = GetParamValueFromContext(0);
            string description = GetParamValueFromContext(1);
            string wellcome = GetParamValueFromContext(2);
            int maxUserCount = GetIntFromString(GetParamValueFromContext(3));
            string member1 = GetParamValueFromContext(4);
            string member2 = GetParamValueFromContext(5);

            List<string> list = new List<string>();
            list.Add(member1);
            list.Add(member2);

            SDKClient.Instance.RoomManager.CreateRoom(name, description, wellcome, maxUserCount, list, new ValueCallBack<Room>(
                onSuccess: (room) => {
                    Console.WriteLine($"CreateRoom success.");
                    Console.WriteLine($"roomId: {room.RoomId}");
                    Console.WriteLine($"name: {room.Name}");
                    Console.WriteLine($"Description: {room.Description}");
                    Console.WriteLine($"Announcement: {room.Announcement}");

                    Console.WriteLine($"AdminList num: {room.AdminList.Count}");
                    foreach(var it in room.AdminList)
                    {
                        Console.WriteLine($"admin item: {it}");
                    }

                    Console.WriteLine($"MemberList num: {room.MemberList.Count}");
                    foreach (var it in room.MemberList)
                    {
                        Console.WriteLine($"member item: {it}");
                    }

                    Console.WriteLine($"BlockList num: {room.BlockList.Count}");
                    foreach (var it in room.BlockList)
                    {
                        Console.WriteLine($"block item: {it}");
                    }

                    Console.WriteLine($"MuteList num: {room.MuteList.Count}");
                    foreach (var it in room.MuteList)
                    {
                        Console.WriteLine($"mute item: {it}");
                    }

                    Console.WriteLine($"MaxUsers: {room.MaxUsers}");
                    Console.WriteLine($"Owner: {room.Owner}");
                    Console.WriteLine($"IsAllMemberMuted: {room.IsAllMemberMuted}");
                    Console.WriteLine($"PermissionType: {room.PermissionType}");
                },
                onError: (code, desc) => {
                    Console.WriteLine($"CreateRoom failed, code:{code}, desc:{desc}");
                }
            ));
        }

        public void CallFunc_IRoomManager_DestroyRoom()
        {
            string roomId = GetParamValueFromContext(0);
            SDKClient.Instance.RoomManager.DestroyRoom(roomId, new CallBack(
                onSuccess: () => {
                    Console.WriteLine($"DestroyRoom success.");
                },
                onError: (code, desc) => {
                    Console.WriteLine($"DestroyRoom failed, code:{code}, desc:{desc}");
                }
            ));
        }

        public void CallFunc_IRoomManager_FetchPublicRoomsFromServer()
        {
            int pageNum = GetIntFromString(GetParamValueFromContext(0));
            int pageSize = GetIntFromString(GetParamValueFromContext(1));
            SDKClient.Instance.RoomManager.FetchPublicRoomsFromServer(pageNum, pageSize, handle: new ValueCallBack<PageResult<Room>>(
                onSuccess: (result) => {
                    int count = 1;
                    foreach (var room in result.Data)
                    {
                        Console.WriteLine($"Count {count}:");

                        Console.WriteLine($"Room id: {room.RoomId}");
                        Console.WriteLine($"name: {room.Name}");
                        Console.WriteLine($"Description: {room.Description}");
                        Console.WriteLine($"Announcement: {room.Announcement}");

                        Console.WriteLine($"AdminList num: {room.AdminList.Count}");
                        foreach (var it in room.AdminList)
                        {
                            Console.WriteLine($"admin item: {it}");
                        }

                        Console.WriteLine($"MemberList num: {room.MemberList.Count}");
                        foreach (var it in room.MemberList)
                        {
                            Console.WriteLine($"member item: {it}");
                        }

                        Console.WriteLine($"BlockList num: {room.BlockList.Count}");
                        foreach (var it in room.BlockList)
                        {
                            Console.WriteLine($"block item: {it}");
                        }

                        Console.WriteLine($"MuteList num: {room.MuteList.Count}");
                        foreach (var it in room.MuteList)
                        {
                            Console.WriteLine($"mute item: {it}");
                        }

                        Console.WriteLine($"MaxUsers: {room.MaxUsers}");

                        Console.WriteLine($"Owner: {room.Owner}");
                        Console.WriteLine($"IsAllMemberMuted: {room.IsAllMemberMuted}");
                        Console.WriteLine($"PermissionType: {room.PermissionType}");
                        Console.WriteLine($"=====================================");
                        count++;
                    }

                },
                onError: (code, desc) => {
                    Console.WriteLine($"FetchPublicRoomsFromServer failed, code:{code}, desc:{desc}");
                }
            ));
        }

        public void CallFunc_IRoomManager_FetchRoomAnnouncement()
        {
            string roomId = GetParamValueFromContext(0);
            SDKClient.Instance.RoomManager.FetchRoomAnnouncement(roomId, new ValueCallBack<string>(
                onSuccess: (str) => {
                    Console.WriteLine($"FetchRoomAnnouncement success, annoucement:{str}");
                },
                onError: (code, desc) => {
                    Console.WriteLine($"FetchRoomAnnouncement failed, code:{code}, desc:{desc}");
                }
            ));
        }

        public void CallFunc_IRoomManager_FetchRoomBlockList()
        {
            string roomId = GetParamValueFromContext(0);
            int pageNum = GetIntFromString(GetParamValueFromContext(1));
            int pageSzie = GetIntFromString(GetParamValueFromContext(2));
            SDKClient.Instance.RoomManager.FetchRoomBlockList(roomId, pageNum, pageSzie, handle: new ValueCallBack<List<string>>(
                onSuccess: (list) => {
                    Console.WriteLine($"FetchRoomBlockList success, block num: {list.Count}");
                    foreach(var it in list)
                    {
                        Console.WriteLine($"block item: {it}");
                    }
                },
                onError: (code, desc) => {
                    Console.WriteLine($"FetchRoomBlockList failed, code:{code}, desc:{desc}");
                }
            ));
        }

        public void CallFunc_IRoomManager_FetchRoomInfoFromServer()
        {
            string roomId = GetParamValueFromContext(0);

            SDKClient.Instance.RoomManager.FetchRoomInfoFromServer(roomId, new ValueCallBack<Room>(
                onSuccess: (room) => {
                    Console.WriteLine($"FetchRoomInfoFromServer success.");
                    Console.WriteLine($"roomId: {room.RoomId}");
                    Console.WriteLine($"name: {room.Name}");
                    Console.WriteLine($"Description: {room.Description}");
                    Console.WriteLine($"Announcement: {room.Announcement}");

                    Console.WriteLine($"AdminList num: {room.AdminList.Count}");
                    foreach (var it in room.AdminList)
                    {
                        Console.WriteLine($"admin item: {it}");
                    }

                    Console.WriteLine($"MemberList num: {room.MemberCount}");
                    foreach (var it in room.MemberList)
                    {
                        Console.WriteLine($"member item: {it}");
                    }

                    Console.WriteLine($"BlockList num: {room.BlockList.Count}");
                    foreach (var it in room.BlockList)
                    {
                        Console.WriteLine($"block item: {it}");
                    }

                    Console.WriteLine($"MuteList num: {room.MuteList.Count}");
                    foreach (var it in room.MuteList)
                    {
                        Console.WriteLine($"mute item: {it}");
                    }

                    Console.WriteLine($"MaxUsers: {room.MaxUsers}");
                    Console.WriteLine($"Owner: {room.Owner}");
                    Console.WriteLine($"IsAllMemberMuted: {room.IsAllMemberMuted}");
                    Console.WriteLine($"PermissionType: {room.PermissionType}");
                },
                onError: (code, desc) => {
                    Console.WriteLine($"CreateRoom failed, code:{code}, desc:{desc}");
                }
            ));
        }

        public void CallFunc_IRoomManager_FetchRoomMembers()
        {
            string roomId = GetParamValueFromContext(0);
            string cursor = GetParamValueFromContext(1);
            int pageSize = GetIntFromString(GetParamValueFromContext(2));
                SDKClient.Instance.RoomManager.FetchRoomMembers(roomId, cursor, pageSize, handle: new ValueCallBack<CursorResult<string>>(
                onSuccess: (result) => {                
                    Console.WriteLine($"FetchRoomMembers success, member num: {result.Data.Count}");
                    foreach(var it in result.Data)
                    {
                        Console.WriteLine($"member: {it}");
                    }
                },
                onError: (code, desc) => {
                    Console.WriteLine($"FetchRoomMembers failed, code:{code}, desc:{desc}");
                }
            ));
        }

        public void CallFunc_IRoomManager_FetchRoomMuteList()
        {
            string roomId = GetParamValueFromContext(0);
            int pageSize = GetIntFromString(GetParamValueFromContext(1));
            int pageNum = GetIntFromString(GetParamValueFromContext(2));
                SDKClient.Instance.RoomManager.FetchRoomMuteList(roomId, pageSize, pageNum, handle: new ValueCallBack<List<string>>(
                 onSuccess: (result) => {
                     Console.WriteLine($"FetchRoomMuteList success, mute count：{result.Count}");
                     foreach(var it in result)
                     {
                         Console.WriteLine($"mute item: {it}");
                     }
                 },
                 onError: (code, desc) => {
                     Console.WriteLine($"FetchRoomMembers failed, code:{code}, desc:{desc}");
                 }
             ));
        }

        public void CallFunc_IRoomManager_JoinRoom()
        {
            string roomId = GetParamValueFromContext(0);

            SDKClient.Instance.RoomManager.JoinRoom(roomId, new ValueCallBack<Room>(
                onSuccess: (room) => {
                    Console.WriteLine($"JoinRoom success.");
                    Console.WriteLine($"roomId: {room.RoomId}");
                    Console.WriteLine($"name: {room.Name}");
                    Console.WriteLine($"Description: {room.Description}");
                    Console.WriteLine($"Announcement: {room.Announcement}");

                    Console.WriteLine($"AdminList num: {room.AdminList.Count}");
                    foreach (var it in room.AdminList)
                    {
                        Console.WriteLine($"admin item: {it}");
                    }

                    Console.WriteLine($"MemberList num: {room.MemberList.Count}");
                    foreach (var it in room.MemberList)
                    {
                        Console.WriteLine($"member item: {it}");
                    }

                    Console.WriteLine($"BlockList num: {room.BlockList.Count}");
                    foreach (var it in room.BlockList)
                    {
                        Console.WriteLine($"block item: {it}");
                    }

                    Console.WriteLine($"MuteList num: {room.MuteList.Count}");
                    foreach (var it in room.MuteList)
                    {
                        Console.WriteLine($"mute item: {it}");
                    }

                    Console.WriteLine($"MaxUsers: {room.MaxUsers}");
                    Console.WriteLine($"Owner: {room.Owner}");
                    Console.WriteLine($"IsAllMemberMuted: {room.IsAllMemberMuted}");
                    Console.WriteLine($"PermissionType: {room.PermissionType}");
                },
                onError: (code, desc) => {
                    Console.WriteLine($"JoinRoom failed, code:{code}, desc:{desc}");
                }
            ));
        }

        public void CallFunc_IRoomManager_LeaveRoom()
        {
            string roomId = GetParamValueFromContext(0);
            SDKClient.Instance.RoomManager.LeaveRoom(roomId, new CallBack(
                onSuccess: () => {
                    Console.WriteLine($"LeaveRoom success.");
                },
                onError: (code, desc) => {
                    Console.WriteLine($"LeaveRoom failed, code:{code}, desc:{desc}");
                }
            ));
        }

        public void CallFunc_IRoomManager_MuteRoomMembers()
        {
            string roomId = GetParamValueFromContext(0);
            string member1 = GetParamValueFromContext(1);
            string member2 = GetParamValueFromContext(2);

            List<string> list = new List<string>();
            list.Add(member1);
            list.Add(member2);

            SDKClient.Instance.RoomManager.MuteRoomMembers(roomId, list, new CallBack(
                onSuccess: () => {
                    Console.WriteLine($"MuteRoomMembers success.");
                },
                onError: (code, desc) => {
                    Console.WriteLine($"MuteRoomMembers failed, code:{code}, desc:{desc}");
                }
            ));
        }

        public void CallFunc_IRoomManager_RemoveRoomAdmin()
        {
            string roomId = GetParamValueFromContext(0);
            string adminId = GetParamValueFromContext(1);

            SDKClient.Instance.RoomManager.RemoveRoomAdmin(roomId, adminId, new CallBack(
                onSuccess: () => {
                    Console.WriteLine($"RemoveRoomAdmin success.");
                },
                onError: (code, desc) => {
                    Console.WriteLine($"RemoveRoomAdmin failed, code:{code}, desc:{desc}");
                }
            ));
        }

        public void CallFunc_IRoomManager_DeleteRoomMembers()
        {
            string roomId = GetParamValueFromContext(0);
            string member1 = GetParamValueFromContext(1);
            string member2 = GetParamValueFromContext(2);

            List<string> list = new List<string>();
            list.Add(member1);
            list.Add(member2);

            SDKClient.Instance.RoomManager.DeleteRoomMembers(roomId, list, new CallBack(
                onSuccess: () => {
                    Console.WriteLine($"DeleteRoomMembers success.");
                },
                onError: (code, desc) => {
                    Console.WriteLine($"DeleteRoomMembers failed, code:{code}, desc:{desc}");
                }
            ));
        }

        public void CallFunc_IRoomManager_UnBlockRoomMembers()
        {
            string roomId = GetParamValueFromContext(0);
            string member1 = GetParamValueFromContext(1);
            string member2 = GetParamValueFromContext(2);

            List<string> list = new List<string>();
            list.Add(member1);
            list.Add(member2);

            SDKClient.Instance.RoomManager.UnBlockRoomMembers(roomId, list, new CallBack(
                onSuccess: () => {
                    Console.WriteLine($"UnBlockRoomMembers success.");
                },
                onError: (code, desc) => {
                    Console.WriteLine($"UnBlockRoomMembers failed, code:{code}, desc:{desc}");
                }
            ));
        }

        public void CallFunc_IRoomManager_UnMuteRoomMembers()
        {
            string roomId = GetParamValueFromContext(0);
            string member1 = GetParamValueFromContext(1);
            string member2 = GetParamValueFromContext(2);

            List<string> list = new List<string>();
            list.Add(member1);
            list.Add(member2);

            SDKClient.Instance.RoomManager.UnMuteRoomMembers(roomId, list, new CallBack(
                onSuccess: () => {
                    Console.WriteLine($"UnMuteRoomMembers success.");
                },
                onError: (code, desc) => {
                    Console.WriteLine($"UnMuteRoomMembers failed, code:{code}, desc:{desc}");
                }
            ));
        }

        public void CallFunc_IRoomManager_UpdateRoomAnnouncement()
        {
            string roomId = GetParamValueFromContext(0);
            string annoucement = GetParamValueFromContext(1);

            SDKClient.Instance.RoomManager.UpdateRoomAnnouncement(roomId, annoucement, new CallBack(
                onSuccess: () => {
                    Console.WriteLine($"UpdateRoomAnnouncement success.");
                },
                onError: (code, desc) => {
                    Console.WriteLine($"UpdateRoomAnnouncement failed, code:{code}, desc:{desc}");
                }
            ));
        }

        public void CallFunc_IRoomManager()
        {
            if (select_context.level2_item.CompareTo("AddRoomAdmin") == 0)
            {
                CallFunc_IRoomManager_AddRoomAdmin();
                return;
            }

            if (select_context.level2_item.CompareTo("BlockRoomMembers") == 0)
            {
                CallFunc_IRoomManager_BlockRoomMembers();
                return;
            }

            if (select_context.level2_item.CompareTo("ChangeRoomOwner") == 0)
            {
                CallFunc_IRoomManager_ChangeRoomOwner();
                return;
            }

            if (select_context.level2_item.CompareTo("ChangeRoomDescription") == 0)
            {
                CallFunc_IRoomManager_ChangeRoomDescription();
                return;
            }

            if (select_context.level2_item.CompareTo("ChangeRoomName") == 0)
            {
                CallFunc_IRoomManager_ChangeRoomName();
                return;
            }

            if (select_context.level2_item.CompareTo("CreateRoom") == 0)
            {
                CallFunc_IRoomManager_CreateRoom();
                return;
            }

            if (select_context.level2_item.CompareTo("DestroyRoom") == 0)
            {
                CallFunc_IRoomManager_DestroyRoom();
                return;
            }

            if (select_context.level2_item.CompareTo("FetchPublicRoomsFromServer") == 0)
            {
                CallFunc_IRoomManager_FetchPublicRoomsFromServer();
                return;
            }

            if (select_context.level2_item.CompareTo("FetchRoomAnnouncement") == 0)
            {
                CallFunc_IRoomManager_FetchRoomAnnouncement();
                return;
            }

            if (select_context.level2_item.CompareTo("FetchRoomBlockList") == 0)
            {
                CallFunc_IRoomManager_FetchRoomBlockList();
                return;
            }

            if (select_context.level2_item.CompareTo("FetchRoomInfoFromServer") == 0)
            {
                CallFunc_IRoomManager_FetchRoomInfoFromServer();
                return;
            }

            if (select_context.level2_item.CompareTo("FetchRoomMembers") == 0)
            {
                CallFunc_IRoomManager_FetchRoomMembers();
                return;
            }

            if (select_context.level2_item.CompareTo("FetchRoomMuteList") == 0)
            {
                CallFunc_IRoomManager_FetchRoomMuteList();
                return;
            }

            if (select_context.level2_item.CompareTo("JoinRoom") == 0)
            {
                CallFunc_IRoomManager_JoinRoom();
                return;
            }

            if (select_context.level2_item.CompareTo("LeaveRoom") == 0)
            {
                CallFunc_IRoomManager_LeaveRoom();
                return;
            }

            if (select_context.level2_item.CompareTo("MuteRoomMembers") == 0)
            {
                CallFunc_IRoomManager_MuteRoomMembers();
                return;
            }

            if (select_context.level2_item.CompareTo("RemoveRoomAdmin") == 0)
            {
                CallFunc_IRoomManager_RemoveRoomAdmin();
                return;
            }

            if (select_context.level2_item.CompareTo("DeleteRoomMembers") == 0)
            {
                CallFunc_IRoomManager_DeleteRoomMembers();
                return;
            }

            if (select_context.level2_item.CompareTo("UnBlockRoomMembers") == 0)
            {
                CallFunc_IRoomManager_UnBlockRoomMembers();
                return;
            }

            if (select_context.level2_item.CompareTo("UnMuteRoomMembers") == 0)
            {
                CallFunc_IRoomManager_UnMuteRoomMembers();
                return;
            }

            if (select_context.level2_item.CompareTo("UpdateRoomAnnouncement") == 0)
            {
                CallFunc_IRoomManager_UpdateRoomAnnouncement();
                return;
            }
        }

        public void CallFunc_IUserInfoManager_UpdateOwnInfo()
        {
            string userId = GetParamValueFromContext(0);
            string nickName = GetParamValueFromContext(1);
            string avatarUrl = GetParamValueFromContext(2);
            string email = GetParamValueFromContext(3);
            int gender = GetIntFromString(GetParamValueFromContext(4));

            UserInfo userInfo = new UserInfo();
            userInfo.userId = userId;
            userInfo.nickName = nickName;
            userInfo.avatarUrl = avatarUrl;
            userInfo.email = email;
            userInfo.gender = gender;

            SDKClient.Instance.UserInfoManager.UpdateOwnInfo(userInfo, new CallBack(
                onSuccess: () => {
                    Console.WriteLine($"UpdateOwnInfo success.");
                },
                onError: (code, desc) => {
                    Console.WriteLine($"UpdateOwnInfo failed, code:{code}, desc:{desc}");
                }
            ));
        }

        public void CallFunc_IUserInfoManager_FetchUserInfoByUserId()
        {
            string userId1 = GetParamValueFromContext(0);
            string userId2 = GetParamValueFromContext(1);

            List<string> list = new List<string>();
            list.Add(userId1);
            list.Add(userId2);

            SDKClient.Instance.UserInfoManager.FetchUserInfoByUserId(list, new ValueCallBack<Dictionary<string, UserInfo>>(
                onSuccess: (dict) => {
                    Console.WriteLine($"FetchUserInfoByUserId success.");
                    foreach(var it in dict)
                    {
                        Console.WriteLine($"user name: {it.Key}");
                        UserInfo ui = it.Value;
                        Console.WriteLine($"userId: {ui.userId}");
                        Console.WriteLine($"nickName: {ui.nickName}");
                        Console.WriteLine($"avatarUrl: {ui.avatarUrl}");
                        Console.WriteLine($"email: {ui.email}");
                        Console.WriteLine($"phoneNumber: {ui.phoneNumber}");
                        Console.WriteLine($"signature: {ui.signature}");
                        Console.WriteLine($"birth: {ui.birth}");
                        Console.WriteLine($"ext: {ui.ext}");
                        Console.WriteLine($"gender: {ui.gender}");
                        Console.WriteLine($"===============================");
                    }
                },
                onError: (code, desc) => {
                    Console.WriteLine($"FetchUserInfoByUserId failed, code:{code}, desc:{desc}");
                }
            ));
        }

        public void CallFunc_IUserInfoManager()
        {
            if (select_context.level2_item.CompareTo("UpdateOwnInfo") == 0)
            {
                CallFunc_IUserInfoManager_UpdateOwnInfo();
                return;
            }

            if (select_context.level2_item.CompareTo("FetchUserInfoByUserId") == 0)
            {
                CallFunc_IUserInfoManager_FetchUserInfoByUserId();
                return;
            }
        }

        public void CallFunc()
        {
            if(select_context.level1_item.CompareTo("IClient") == 0)
            {
                CallFunc_IClient();
                return;
            }

            if (select_context.level1_item.CompareTo("IChatManager") == 0)
            {
                CallFunc_IChatManager();
                return;
            }

            if (select_context.level1_item.CompareTo("IContactManager") == 0)
            {
                CallFunc_IContactManager();
                return;
            }

            if (select_context.level1_item.CompareTo("IConversationManager") == 0)
            {
                CallFunc_IConversationManager();
                return;
            }

            if (select_context.level1_item.CompareTo("IGroupManager") == 0)
            {
                CallFunc_IGroupManager();
                return;
            }

            if (select_context.level1_item.CompareTo("IPushManager") == 0)
            {
                CallFunc_IPushManager();
                return;
            }

            if (select_context.level1_item.CompareTo("IRoomManager") == 0)
            {
                CallFunc_IRoomManager();
                return;
            }

            if (select_context.level1_item.CompareTo("IUserInfoManager") == 0)
            {
                CallFunc_IUserInfoManager();
                return;
            }

        }
    }

    public class ChatManagerDelegate : IChatManagerDelegate
    {
        public void OnMessagesReceived(List<Message> messages)
        {
            Console.WriteLine("OnMessagesReceived");
            foreach(var it in messages)
            {
                Console.WriteLine($"===========================");
                Console.WriteLine($"message id: {it.MsgId}");
                Console.WriteLine($"cov id: {it.ConversationId}");
                Console.WriteLine($"From: {it.From}");
                Console.WriteLine($"To: {it.To}");
                Console.WriteLine($"RecallBy: {it.RecallBy}");
                Console.WriteLine($"message type: {it.MessageType}");
                Console.WriteLine($"diection: {it.Direction}");
                Console.WriteLine($"status: {it.Status}");
                Console.WriteLine($"localtime: {it.LocalTime}");
                Console.WriteLine($"servertime: {it.ServerTime}");
                Console.WriteLine($"HasDeliverAck: {it.HasDeliverAck}");
                Console.WriteLine($"HasReadAck: {it.HasReadAck}");
                foreach(var it1 in it.Attributes)
                {
                    Console.WriteLine($"attribute item: key:{it1.Key}; value:{it1.Value}");
                }
                if(it.Body.Type == MessageBodyType.TXT)
                {
                    TextBody tb = (TextBody)it.Body;
                    Console.WriteLine($"message text content: {tb.Text}");
                }
                Console.WriteLine($"===========================");
            }
        }

        public void OnCmdMessagesReceived(List<Message> messages)
        {
            Console.WriteLine("OnCmdMessagesReceived");
            foreach (var it in messages)
            {
                Console.WriteLine($"===========================");
                Console.WriteLine($"message id: {it.MsgId}");
                Console.WriteLine($"cov id: {it.ConversationId}");
                Console.WriteLine($"From: {it.From}");
                Console.WriteLine($"To: {it.To}");
                Console.WriteLine($"RecallBy: {it.RecallBy}");
                Console.WriteLine($"message type: {it.MessageType}");
                Console.WriteLine($"diection: {it.Direction}");
                Console.WriteLine($"status: {it.Status}");
                Console.WriteLine($"localtime: {it.LocalTime}");
                Console.WriteLine($"servertime: {it.ServerTime}");
                Console.WriteLine($"HasDeliverAck: {it.HasDeliverAck}");
                Console.WriteLine($"HasReadAck: {it.HasReadAck}");
                foreach (var it1 in it.Attributes)
                {
                    Console.WriteLine($"attribute item: key:{it1.Key}; value:{it1.Value}");
                }
                Console.WriteLine($"===========================");
            }
        }

        public void OnMessagesRead(List<Message> messages)
        {
            Console.WriteLine("OnMessagesRead");
            foreach (var it in messages)
            {
                Console.WriteLine($"===========================");
                Console.WriteLine($"message id: {it.MsgId}");
                Console.WriteLine($"cov id: {it.ConversationId}");
                Console.WriteLine($"From: {it.From}");
                Console.WriteLine($"To: {it.To}");
                Console.WriteLine($"RecallBy: {it.RecallBy}");
                Console.WriteLine($"message type: {it.MessageType}");
                Console.WriteLine($"diection: {it.Direction}");
                Console.WriteLine($"status: {it.Status}");
                Console.WriteLine($"localtime: {it.LocalTime}");
                Console.WriteLine($"servertime: {it.ServerTime}");
                Console.WriteLine($"HasDeliverAck: {it.HasDeliverAck}");
                Console.WriteLine($"HasReadAck: {it.HasReadAck}");
                foreach (var it1 in it.Attributes)
                {
                    Console.WriteLine($"attribute item: key:{it1.Key}; value:{it1.Value}");
                }
                if (it.Body.Type == MessageBodyType.TXT)
                {
                    TextBody tb = (TextBody)it.Body;
                    Console.WriteLine($"message text content: {tb.Text}");
                }
                Console.WriteLine($"===========================");
            }
        }

        public void OnMessagesDelivered(List<Message> messages)
        {
            Console.WriteLine("OnMessagesDelivered");
            foreach (var it in messages)
            {
                Console.WriteLine($"===========================");
                Console.WriteLine($"message id: {it.MsgId}");
                Console.WriteLine($"cov id: {it.ConversationId}");
                Console.WriteLine($"From: {it.From}");
                Console.WriteLine($"To: {it.To}");
                Console.WriteLine($"RecallBy: {it.RecallBy}");
                Console.WriteLine($"message type: {it.MessageType}");
                Console.WriteLine($"diection: {it.Direction}");
                Console.WriteLine($"status: {it.Status}");
                Console.WriteLine($"localtime: {it.LocalTime}");
                Console.WriteLine($"servertime: {it.ServerTime}");
                Console.WriteLine($"HasDeliverAck: {it.HasDeliverAck}");
                Console.WriteLine($"HasReadAck: {it.HasReadAck}");
                foreach (var it1 in it.Attributes)
                {
                    Console.WriteLine($"attribute item: key:{it1.Key}; value:{it1.Value}");
                }
                if (it.Body.Type == MessageBodyType.TXT)
                {
                    TextBody tb = (TextBody)it.Body;
                    Console.WriteLine($"message text content: {tb.Text}");
                }
                Console.WriteLine($"===========================");
            }
        }

        public void OnMessagesRecalled(List<Message> messages)
        {
            Console.WriteLine("OnMessagesRecalled");
            foreach (var it in messages)
            {
                Console.WriteLine($"===========================");
                Console.WriteLine($"message id: {it.MsgId}");
                Console.WriteLine($"cov id: {it.ConversationId}");
                Console.WriteLine($"From: {it.From}");
                Console.WriteLine($"To: {it.To}");
                Console.WriteLine($"RecallBy: {it.RecallBy}");
                Console.WriteLine($"message type: {it.MessageType}");
                Console.WriteLine($"diection: {it.Direction}");
                Console.WriteLine($"status: {it.Status}");
                Console.WriteLine($"localtime: {it.LocalTime}");
                Console.WriteLine($"servertime: {it.ServerTime}");
                Console.WriteLine($"HasDeliverAck: {it.HasDeliverAck}");
                Console.WriteLine($"HasReadAck: {it.HasReadAck}");
                foreach (var it1 in it.Attributes)
                {
                    Console.WriteLine($"attribute item: key:{it1.Key}; value:{it1.Value}");
                }
                if (it.Body.Type == MessageBodyType.TXT)
                {
                    TextBody tb = (TextBody)it.Body;
                    Console.WriteLine($"message text content: {tb.Text}");
                }
                Console.WriteLine($"===========================");
            }
        }

        public void OnReadAckForGroupMessageUpdated()
        {
            Console.WriteLine("OnReadAckForGroupMessageUpdated");
        }

        public void OnGroupMessageRead(List<GroupReadAck> list)
        {
            Console.WriteLine("OnGroupMessageRead");
            foreach (var it in list)
            {
                Console.WriteLine($"===========================");
                Console.WriteLine($"AckId: {it.AckId}");
                Console.WriteLine($"MsgId: {it.MsgId}");
                Console.WriteLine($"From: {it.From}");
                Console.WriteLine($"Content: {it.Content}");
                Console.WriteLine($"Count: {it.Count}");
                Console.WriteLine($"Timestamp: {it.Timestamp}");
                Console.WriteLine($"===========================");
            }
        }

        public void OnConversationsUpdate()
        {
            Console.WriteLine("OnConversationsUpdate");
        }

        public void OnConversationRead(string from, string to)
        {
            Console.WriteLine("OnConversationRead");
        }
      
    }

    class ConnectionDelegate : IConnectionDelegate
    {
        public void OnConnected()
        {
            Console.WriteLine("OnConnected");
        }

        public void OnDisconnected(int i)
        {
            Console.WriteLine($"OnDisconnected: {i}");
        }

        public void OnTokenExpired()
        {
            Console.WriteLine($"OnTokenExpired");
        }

        public void OnTokenWillExpire()
        {
            Console.WriteLine($"OnTokenWillExpire");
        }
    }

    class ContactManagerDelegate : IContactManagerDelegate
    {
        public void OnContactAdded(string username)
        {
            Console.WriteLine($"OnContactAdded: {username}");
        }

        public void OnContactDeleted(string username)
        {
            Console.WriteLine($"OnContactDeleted: {username}");
        }

        public void OnContactInvited(string username, string reason)
        {
            Console.WriteLine($"OnContactInvited: {username}, reason:{reason}");
        }

        public void OnFriendRequestAccepted(string username)
        {
            Console.WriteLine($"OnFriendRequestAccepted: {username}");
        }

        public void OnFriendRequestDeclined(string username)
        {
            Console.WriteLine($"OnFriendRequestDeclined: {username}");
        }
    }

    class GroupManagerDelegate : IGroupManagerDelegate
    {
        public void OnAdminAddedFromGroup(string groupId, string administrator)
        {
            Console.WriteLine($"OnAdminAddedFromGroup: gid: {groupId}; admin:{administrator}");
        }

        public void OnAdminRemovedFromGroup(string groupId, string administrator)
        {
            Console.WriteLine($"OnAdminRemovedFromGroup: gid: {groupId}; admin:{administrator}");
        }

        public void OnAnnouncementChangedFromGroup(string groupId, string announcement)
        {
            Console.WriteLine($"OnAnnouncementChangedFromGroup: gid: {groupId}; announcement:{announcement}");
        }

        public void OnAutoAcceptInvitationFromGroup(string groupId, string inviter, string inviteMessage)
        {
            Console.WriteLine($"OnAutoAcceptInvitationFromGroup: gid: {groupId}; inviter:{inviter}; inviteMessage:{inviteMessage}");
        }

        public void OnDestroyedFromGroup(string groupId, string groupName)
        {
            Console.WriteLine($"OnDestroyedFromGroup: gid: {groupId}; groupName:{groupName}");
        }

        public void OnInvitationAcceptedFromGroup(string groupId, string invitee, string reason)
        {
            Console.WriteLine($"OnInvitationAcceptedFromGroup: gid: {groupId}; invitee:{invitee}; reason:{reason}");
        }

        public void OnInvitationDeclinedFromGroup(string groupId, string invitee, string reason)
        {
            Console.WriteLine($"OnInvitationDeclinedFromGroup: gid: {groupId}; invitee:{invitee}; reason:{reason}");
        }

        public void OnInvitationReceivedFromGroup(string groupId, string groupName, string inviter, string reason)
        {
            Console.WriteLine($"OnInvitationReceivedFromGroup: gid: {groupId}; groupName:{groupName}; inviter:{inviter}; reason:{reason}");
        }

        public void OnMemberExitedFromGroup(string groupId, string member)
        {
            Console.WriteLine($"OnMemberExitedFromGroup: gid: {groupId}; member:{member}");
        }

        public void OnMemberJoinedFromGroup(string groupId, string member)
        {
            Console.WriteLine($"OnMemberJoinedFromGroup: gid: {groupId}; member:{member}");
        }

        public void OnMuteListAddedFromGroup(string groupId, List<string> mutes, int muteExpire)
        {
            Console.WriteLine($"OnMuteListAddedFromGroup: gid: {groupId}; muteExpire:{muteExpire}");
            foreach(var it in mutes)
            {
                Console.WriteLine($"mute item: {it}");
            }
        }

        public void OnMuteListRemovedFromGroup(string groupId, List<string> mutes)
        {
            Console.WriteLine($"OnMuteListRemovedFromGroup: gid: {groupId}");
            foreach (var it in mutes)
            {
                Console.WriteLine($"mute item: {it}");
            }
        }

        public void OnOwnerChangedFromGroup(string groupId, string newOwner, string oldOwner)
        {
            Console.WriteLine($"OnOwnerChangedFromGroup: gid: {groupId}; newOwner:{newOwner}; oldOwner:{oldOwner}");
        }

        public void OnRequestToJoinAcceptedFromGroup(string groupId, string groupName, string accepter)
        {
            Console.WriteLine($"OnRequestToJoinAcceptedFromGroup: gid: {groupId}; newOwner:{groupName}; oldOwner:{accepter}");
        }

        public void OnRequestToJoinDeclinedFromGroup(string groupId, string groupName, string decliner, string reason)
        {
            Console.WriteLine($"OnRequestToJoinDeclinedFromGroup: gid: {groupId}; newOwner:{groupName}; decliner:{decliner}; reason:{reason}");
        }

        public void OnRequestToJoinReceivedFromGroup(string groupId, string groupName, string applicant, string reason)
        {
            Console.WriteLine($"OnRequestToJoinReceivedFromGroup: gid: {groupId}; newOwner:{groupName}; applicant:{applicant}; reason:{reason}");
        }

        public void OnSharedFileAddedFromGroup(string groupId, GroupSharedFile sharedFile)
        {
            Console.WriteLine($"OnRequestToJoinDeclinedFromGroup: gid: {groupId}; fid:{sharedFile.FileId}; fn:{sharedFile.FileName}");
        }

        public void OnSharedFileDeletedFromGroup(string groupId, string fileId)
        {
            Console.WriteLine($"OnRequestToJoinDeclinedFromGroup: gid: {groupId}; fileId:{fileId}");
        }

        public void OnUserRemovedFromGroup(string groupId, string groupName)
        {
            Console.WriteLine($"OnUserRemovedFromGroup: gid: {groupId}; groupName:{groupName}");
        }
    }

    class MultiDeviceDelegate : IMultiDeviceDelegate
    {
        public void onContactMultiDevicesEvent(MultiDevicesOperation operation, string target, string ext)
        {
            Console.WriteLine($"onContactMultiDevicesEvent: operation: {operation}; target:{target}； ext:{ext}"); 
        }

        public void onGroupMultiDevicesEvent(MultiDevicesOperation operation, string target, List<string> usernames)
        {
            Console.WriteLine($"onGroupMultiDevicesEvent: operation: {operation}; target:{target}");
            foreach(var it in usernames)
            {
                Console.WriteLine($"username: {it}");
            }
        }

        public void undisturbMultiDevicesEvent(string data)
        {
            Console.WriteLine($"data: {data}");
        }
    }

    class RoomManagerDelegate : IRoomManagerDelegate
    {
        public void OnAdminAddedFromRoom(string roomId, string admin)
        {
            Console.WriteLine($"OnAdminAddedFromRoom roomId: {roomId}; admin:{admin}");
        }

        public void OnAdminRemovedFromRoom(string roomId, string admin)
        {
            Console.WriteLine($"OnAdminRemovedFromRoom roomId: {roomId}; admin:{admin}");
        }

        public void OnAnnouncementChangedFromRoom(string roomId, string announcement)
        {
            Console.WriteLine($"OnAnnouncementChangedFromRoom roomId: {roomId}; announcement:{announcement}");
        }

        public void OnDestroyedFromRoom(string roomId, string roomName)
        {
            Console.WriteLine($"OnDestroyedFromRoom roomId: {roomId}; roomName:{roomName}");
        }

        public void OnMemberExitedFromRoom(string roomId, string roomName, string participant)
        {
            Console.WriteLine($"OnMemberExitedFromRoom roomId: {roomId}; roomName:{roomName}; participant:{participant}");
        }

        public void OnMemberJoinedFromRoom(string roomId, string participant)
        {
            Console.WriteLine($"OnMemberJoinedFromRoom roomId: {roomId}; participant:{participant};");
        }

        public void OnMuteListAddedFromRoom(string roomId, List<string> mutes, long expireTime)
        {
            Console.WriteLine($"OnMuteListAddedFromRoom: roomId: {roomId}; expireTime:{expireTime}");
            foreach (var it in mutes)
            {
                Console.WriteLine($"mute item: {it}");
            }
        }

        public void OnMuteListRemovedFromRoom(string roomId, List<string> mutes)
        {
            Console.WriteLine($"OnMuteListRemovedFromRoom: roomId: {roomId}");
            foreach (var it in mutes)
            {
                Console.WriteLine($"mute item: {it}");
            }
        }

        public void OnOwnerChangedFromRoom(string roomId, string newOwner, string oldOwner)
        {
            Console.WriteLine($"OnOwnerChangedFromRoom: roomId: {roomId}; newOwner:{newOwner}; oldOwner:{oldOwner}");
        }

        public void OnRemovedFromRoom(string roomId, string roomName, string participant)
        {
            Console.WriteLine($"OnRemovedFromRoom: roomId: {roomId}; roomName:{roomName}; participant:{participant}");
        }
    }
}
