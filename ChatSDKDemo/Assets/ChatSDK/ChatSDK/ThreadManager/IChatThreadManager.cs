using System.Collections.Generic;

namespace ChatSDK
{
    public abstract class IChatThreadManager
    {
        /**
         * \~chinese
         * 根据子区id获取本地子区信息。
         *
         * 子区所属群组的所有成员均可调用该方法。
         *
         * @param threadId  子区 ID。
         * @param handle    结果回调：
         *                      - 成功时回调 {@link ValueCallBack#onSuccess(Object)}，返回获取结果 {@link CursorResult}，包含成员列表以及用于下次获取数据的游标；
         *                      - 失败时回调 {@link ValueCallBack#onError(int, String)}，返回错误信息。
         *
         * \~chinese
         * Gets a local thread basing on thread id.
         *
         * Each member of the group to which the message thread belongs can call this method.
         *
         * @param threadId   The message thread ID.
         * @param handle     The result callback:
         *                     - If success, {@link ValueCallBack#onSuccess(Object)} is triggered to return the result {@link CursorResult}, including the message thread member list and the cursor for the next query.
         *                     - If a failure occurs, {@link ValueCallBack#onError(int, String)} is triggered to return an error.
         */
        public abstract void GetThreadWithThreadId(string threadId, ValueCallBack<ChatThread> handle = null);
        /**
         * \~chinese
         * 创建子区。
         *
         * 子区所属群组的所有成员均可调用该方法。
         *
         * 子区创建成功后，会出现如下情况：
         *
         * - 单设备登录时，子区所属群组的所有成员均会收到回调 {@link IChatThreadManagerDelegate#OnChatThreadCreate(ChatThreadEvent)}。
         *   你可通过设置 {@link IChatThreadManagerDelegate} 监听相关事件。
         *
         * - 多端多设备登录时，各设备会收到事件回调 {@link IMultiDeviceDelegate#onThreadMultiDevicesEvent(MultiDevicesOperation, String, List)}。
         *   该回调方法中第一个参数表示子区事件，例如，子区创建事件为 {@link MultiDevicesOperation#THREAD_CREATE}。
         *   你可通过设置 {@link IMultiDeviceDelegate} 监听相关事件。
         *
         * @param threadName 要创建的子区的名称。长度不超过 64 个字符。
         * @param msgId 父消息 ID。
         * @param groupId 父 ID，即群组 ID。
         *          
         * @param handle      结果回调：
         *                  - 成功时回调 {@link ValueCallBack#onSuccess(Object)}，返回创建成功的子区对象；
         *                  - 失败时回调 {@link ValueCallBack#onError(int, String)}，返回错误信息。
         *
         * \~english
         * Creates a message thread.
         *
         * Each member of the chat group where the message thread belongs can call this method.
         *
         * Upon the creation of a message thread, the following will occur:
         *
         * - In a single-device login scenario, each member of the group to which the message thread belongs will receive the {@link IChatThreadManagerDelegate#OnChatThreadCreate(ChatThreadEvent)} callback.
         *   You can listen for message thread events by setting {@link EMChatThreadChangeListener}.
         *
         * - In a multi-device login scenario, the devices will receive the {@link IMultiDeviceDelegate#onThreadMultiDevicesEvent(MultiDevicesOperation, String, List)} callback.
         *   In this callback method, the first parameter indicates a message thread event, for example, {@link MultiDevicesOperation#THREAD_CREATE} for a message thread creation event.
         *   You can listen for message thread events by setting {@link IMultiDeviceDelegate}.
         *
         * @param threadName The name of the new message thread. It can contain a maximum of 64 characters.
         * @param msgId     The ID of the parent message.
         * @param groupId              The parent ID, which is the group ID.
         * 
         * 
         * @param handle      The result callback:
         *                    - If success, {@link EMValueCallBack#onSuccess(Object)} is triggered to return the new message thread object;
         *                    - If a failure occurs, {@link EMValueCallBack#onError(int, String)} is triggered to return an error.
         */
        public abstract void CreateThread(string threadName, string msgId, string groupId, ValueCallBack<ChatThread> handle = null);

        /**
         * \~chinese
         * 加入子区。
         *
         * 子区所属群组的所有成员均可调用该方法。
         *
         * 多端多设备登录时，注意以下几点：
         *
         * - 设备会收到 {@link IMultiDeviceDelegate#onThreadMultiDevicesEvent(MultiDevicesOperation, String, List)} 回调。
         *
         * - 该回调方法中，第一个参数表示子区事件，例如，加入子区事件为 {@link MultiDevicesOperation#THREAD_JOIN}。
         *
         * - 可通过设置 {@link IMultiDeviceDelegate} 监听相关事件。
         *
         * @param threadId 子区 ID。
         * @param handle   结果回调：
         *                     - 成功时回调 {@link ValueCallBack#onSuccess(Object)}，返回子区详情 {@link ChatThread}，详情中不含成员数量；
         *                     - 失败时回调 {@link ValueCallBack#onError(int, String)}，返回错误信息。
         *                       重复加入会报错，错误内容为USER_ALREADY_EXIST。
         *
         * \~english
         * Joins a message thread.
         *
         * Each member of the group where the message thread belongs can call this method.
         *
         * In a multi-device login scenario, note the following:
         *
         * - The devices will receive the {@link IMultiDeviceDelegate#onThreadMultiDevicesEvent(MultiDevicesOperation, String, List)} callback.
         *
         * - In this callback method, the first parameter indicates a message thread event, for example, {@link MultiDevicesOperation#THREAD_JOIN} for a message thread join event.
         *
         * - You can listen for message thread events by setting {@link IMultiDeviceDelegate}.
         *
         * @param threadId   The message thread ID.
         * @param handle     The result callback:
         *                     - If success, {@link ValueCallBack#onSuccess(Object)} is triggered to return the message thread details {@link ChatThread} which do not include the member count.
         *                     - If a failure occurs, {@link ValueCallBack#onError(int, String)} is triggered to return an error.
         *                       If you join a message thread repeatedly, the SDK will return an error related to USER_ALREADY_EXIST.
         */
        public abstract void JoinThread(string threadId, ValueCallBack<ChatThread> handle = null);

        /**
        * \~chinese
        * 退出子区。
        *
        * 子区中的所有成员均可调用该方法。
        *
        * 多设备登录情况下，注意以下几点：
        *
        * - 各设备会收到 {@link IMultiDeviceDelegate#onThreadMultiDevicesEvent(MultiDevicesOperation, String, List)} 回调。
        *
        * - 该回调方法中第一个参数表示事件，退出子区事件为 {@link MultiDevicesOperation#THREAD_LEAVE}。
        *
        * - 你可通过设置 {@link IMultiDeviceDelegate} 监听相关事件；
        *
        * @param threadId     要退出的子区的 ID。
        * @param handle       结果回调：
        *                     - 成功时回调 {@link CallBack#onSuccess()}；
        *                     - 失败时回调 {@link CallBack#onError(int, String)}，返回错误信息。
        *
        * \~english
        * Leaves a message thread.
        *
        * Each member in the message thread can call this method.
        *
        * In a multi-device login scenario, note the following:
        *
        * - The devices will receive the {@link IMultiDeviceDelegate#onThreadMultiDevicesEvent(MultiDevicesOperation, String, List)} callback.
        *
        * - In this callback method, the first parameter indicates a message thread event, for example, {@link MultiDevicesOperation#THREAD_LEAVE} for a message thread leaving event.
        *
        * - You can listen for message thread events by setting {@link IMultiDeviceDelegate}.
        *
        * @param threadId     The ID of the message thread that the current user wants to leave.
        * @param handle       The result callback:
        *                     - If success, {@link CallBack#onSuccess()} is triggered;
        *                     - If a failure occurs, {@link CallBack#onError(int, String)} is triggered to return an error.
        */
        public abstract void LeaveThread(string threadId, CallBack handle = null);

        /**
        * \~chinese
        * 解散子区。
        *
        * 只有子区所属群组的群主及管理员可调用该方法。
        *
        * **注意**
        *
        * - 多端多设备登录时，设备会收到 {@link IMultiDeviceDelegate#onThreadMultiDevicesEvent(MultiDevicesOperation, String, List)} 回调。
        *   该回调方法中，第一个参数为子区事件，例如，子区解散事件为 {@link MultiDevicesOperation#THREAD_DESTROY}。
        *   你可通过设置 {@link IMultiDeviceDelegate} 监听子区事件。
        *
        *
        * @param threadId 子区 ID。
        * @param handle     结果回调：
        *                     - 成功时回调 {@link CallBack#onSuccess()}；
        *                     - 失败时回调 {@link CallBack#onError(int, String)}，返回错误信息。
        *
        * \~english
        * Destroys the message thread.
        *
        * Only the owner or admins of the group where the message thread belongs can call this method.
        *
        * **Note**
        *
        *
        * - In a multi-device login scenario, The devices will receive the {@link IMultiDeviceDelegate#onThreadMultiDevicesEvent(MultiDevicesOperation, String, List)} callback.
        *   In this callback method, the first parameter indicates a message thread event, for example, {@link MultiDevicesOperation#THREAD_DESTROY} for a message thread destruction event.
        *   You can listen for message thread events by setting {@link IMultiDeviceDelegate}.
        *
        * @param threadId  The message thread ID.
        * @param handle    The result callback:
        *                  - If success, {@link CallBack#onSuccess()} is triggered.
        *                   - If a failure occurs, {@link CallBack#onError(int, String)} is triggered to return an error.
        */

        public abstract void DestroyThread(string threadId, CallBack handle = null);

        /**
        * \~chinese
        * 移除子区成员。
        *
        * 只有子区所属群主、群管理员及子区创建者可调用该方法。
        *
        * 被移出的成员会收到 {@link IChatThreadManagerDelegate#OnUserKickOutOfChatThread(ChatThreadEvent)} 回调。
        *
        * 你可通过设置 {@link IChatThreadManagerDelegate} 监听子区事件。
        *
        *
        * @param threadId  子区 ID。
        * @param username  被移出子区的成员的用户 ID。
        * @param handle    结果回调。
        *                    - 成功时回调 {@link CallBack#onSuccess()}；
        *                    - 失败时回调 {@link CallBack#onError(int, String)}，返回错误信息。
        *
        * \~english
        * Removes a member from the message thread.
        *
        * Only the owner or admins of the group where the message thread belongs and the message thread creator can call this method.
        *
        * The removed member will receive the {@link IChatThreadManagerDelegate#OnUserKickOutOfChatThread(ChatThreadEvent)} callback.
        *
        * You can listen for message thread events by setting {@link IChatThreadManagerDelegate}.
        *
        * @param threadId  The message thread ID.
        * @param username  The user ID of the member to be removed from the message thread.
        * @param handle    The result callback.
        *                     - If success, {@link CallBack#onSuccess()} is triggered.
        *                     - If a failure occurs, {@link CallBack#onError(int, String)} is triggered to return an error.
        */
        public abstract void RemoveThreadMember(string threadId, string username, CallBack handle = null);

        /**
         * \~chinese
         * 修改子区名称。
         *
         * 只有子区所属群主、群管理员及子区创建者可调用该方法。
         *
         * 子区所属群组的成员会收到 {@link IChatThreadManagerDelegate#OnChatThreadUpdate(ChatThreadEvent)} 回调。
         *
         * 你可通过设置 {@link IChatThreadManagerDelegate} 监听子区事件。
         *
         * @param threadId        子区 ID。
         * @param newName      子区的新名称。长度不超过 64 个字符。
         * @param handle          结果回调：
         *                        - 成功时回调 {@link CallBack#onSuccess()}；
         *                        - 失败时回调 {@link CallBack#onError(int, String)}，返回错误信息。
         *
         * \~english
         * Changes the name of the message thread.
         *
         * Only the owner or admins of the group where the message thread belongs and the message thread creator can call this method.
         *
         * Each member of the group to which the message thread belongs will receive the {@link IChatThreadManagerDelegate#OnChatThreadUpdate(ChatThreadEvent)} callback.
         *
         * You can listen for message thread events by setting {@link IChatThreadManagerDelegate}.
         *
         * @param threadId      The message thread ID.
         * @param newName       The new message thread name. It can contain a maximum of 64 characters.
         * @param handle        The result callback:
         *                      - If success, {@link CallBack#onSuccess()} is triggered.
         *                      - If a failure occurs, {@link CallBack#onError(int, String)} is triggered to return an error.
         */
        public abstract void ChangeThreadName(string threadId, string newName, CallBack handle = null);

        /**
         * \~chinese
         * 分页获取子区成员。
         *
         * 子区所属群组的所有成员均可调用该方法。
         *
         * @param threadId  子区 ID。
         * @param cursor    开始获取数据的游标位置，首次调用方法时传 `null` 或空字符串，按成员加入子区时间的正序获取数据。
         * @param pageSize  每页期望返回的成员数。取值范围为 [1,50]。
         * @param callBack  结果回调：
         *                      - 成功时回调 {@link ValueCallBack#onSuccess(Object)}，返回获取结果 {@link CursorResult}，包含成员列表以及用于下次获取数据的游标；
         *                      - 失败时回调 {@link ValueCallBack#onError(int, String)}，返回错误信息。
         *
         * \~chinese
         * Gets a list of members in the message thread with pagination.
         *
         * Each member of the group to which the message thread belongs can call this method.
         *
         * @param threadId The message thread ID.
         * 
         * @param cursor       The position from which to start getting data. At the first method call, if you set `cursor` to `null` or an empty string, the SDK will get data in the chronological order of when members join the message thread.
         * @param pageSize     The number of members that you expect to get on each page. The value range is [1,50].
         * @param callBack     The result callback:
         *                     - If success, {@link ValueCallBack#onSuccess(Object)} is triggered to return the result {@link CursorResult}, including the message thread member list and the cursor for the next query.
         *                     - If a failure occurs, {@link ValueCallBack#onError(int, String)} is triggered to return an error.
         */
        public abstract void FetchThreadMembers(string threadId, string cursor = null, int pageSize = 20, ValueCallBack<CursorResult<string>> handle = null);

        /**
        * \~chinese
        * 分页从服务器端获取指定群组的子区列表。
        *
        * @param groupId       父 ID，即群组 ID。
        * @param joined        是否加入的子区。  
        * @param cursor        开始取数据的游标位置。首次获取数据时传 `null` 或空字符串，按子区创建时间的倒序获取数据。
        * @param pageSize      每页期望返回的子区数。取值范围为 [1,50]。
        * @param callBack      结果回调：
        *                         - 成功时回调 {@link ValueCallBack#onSuccess(Object)}，返回获取结果 {@link CursorResult}，包含子区列表和下次查询的游标。
        *                         - 失败时回调 {@link ValueCallBack#onError(int, String)}，返回错误信息。
        *
        * \~english
        * Use the pagination to get the list of message threads in the specified group.
        *
        * This method gets data from the server.
        *
        * @param groupId   The parent ID, which is the group ID.       
        * @param joined    The threads are joined or not.
        * @param cursor    The position from which to start getting data. At the first method call, if you set `cursor` to `null` or an empty string, the SDK will get data in the reverse chronological order of when message threads are created.
        * @param pageSize  The number of message threads that you expect to get on each page. The value range is [1,50].
        * @param callBack  The result callback:
        *                  - If success, {@link ValueCallBack#onSuccess(Object)} is triggered to return the result {@link CursorResult}, including the message thread list and the cursor for the next query.
        *                  - If a failure occurs, {@link ValueCallBack#onError(int, String)} is triggered to return an error.
        */

        public abstract void FetchThreadListOfGroup(string groupId, bool joined, string cursor = null, int pageSize = 20, ValueCallBack<CursorResult<ChatThread>> handle = null);

        /**
         * \~chinese
         * 分页从服务器获取当前用户加入的子区列表。
         *
         * @param cursor      开始获取数据的游标位置。首次调用方法时传 `null` 或空字符串，按用户加入子区时间的倒序获取数据。
         * @param pageSize    每页期望返回的子区数。取值范围为 [1,50]。
         * @param callBack    结果回调：
         *                       - 成功时回调 {@link ValueCallBack#onSuccess(Object)}，返回分页获取结果 {@link CursorResult}，包含子区列表以及用于下次获取数据的游标；
         *                        - 失败时回调 {@link ValueCallBack#onError(int, String)}，返回错误信息。
         *
         * \~english
         * Uses the pagination to get the list of message threads that the current user has joined.
         *
         * This method gets data from the server.
         *         
         * @param cursor    The position from which to start getting data. At the first method call, if you set `cursor` to `null` or an empty string, the SDK will get data in the reverse chronological order of when the user joins the message threads.
         * @param pageSize     The number of message threads that you expect to get on each page. The value range is [1,50].
         * @param callBack  The result callback:
         *                  - If success, {@link ValueCallBack#onSuccess(Object)} is triggered to return the result {@link CursorResult}, including the message thread list and the cursor for the next query.
         *                  - If a failure occurs, {@link ValueCallBack#onError(int, String)} is triggered to return an error.
         */

        public abstract void FetchMineJoinedThreadList(string cursor = null, int pageSize = 20, ValueCallBack<CursorResult<ChatThread>> handle = null);
        
        /**
         * \~chinese
         * 从服务器获取子区详情。
         *
         * @param threadId 子区 ID。
         * @param handle     结果回调：
         *                     - 成功时回调 {@link ValueCallBack#onSuccess(Object)}，返回子区详情；
         *                     - 失败时回调 {@link ValueCallBack#onError(int, String)}，返回错误信息。
         *
         * \~english
         * Gets the details of the message thread from the server.
         *
         * @param threadId   The message thread ID.
         * @param handle       The result callback:
         *                       - If success, {@link ValueCallBack#onSuccess(Object)} is triggered to return the message thread details;
         *                       - If a failure occurs, {@link ValueCallBack#onError(int, String)} is triggered to return an error.
         */
        public abstract void GetThreadDetail(string threadId, ValueCallBack<ChatThread> handle = null);

        /**
         * \~chinese
         * 从服务器批量获取指定子区中的最新一条消息。
         *
         * @param threadIds 要查询的子区 ID 列表，每次最多可传 20 个子区。
         * @param handle    结果回调：
         *                     - 成功时回调 {@link ValueCallBack#onSuccess(Object)}，返回一个 Map 集合，键为子区 ID，值是对应子区最新消息；
         *                     - 失败时回调 {@link ValueCallBack#onError(int, String)}，返回错误信息。
         *
         * \~chinese
         * Gets the last reply in the specified message threads from the server.
         *
         * @param threadIds The list of message thread IDs to query. You can pass a maximum of 20 message thread IDs each time.
         * @param handle    The result callback:
         *                    - If success, {@link ValueCallBack#onSuccess(Object)} is triggered to return a Map collection that contains key-value pairs where the key is the message thread ID and the value is the last threaded reply.
         *                    - If a failure occurs, {@link ValueCallBack#onError(int, String)} is triggered to return an error.
         */
        public abstract void GetLastMessageAccordingThreads(List<string> threadIds, ValueCallBack<Dictionary<string, Message>> handle = null);

        /**
         * \~chinese
         * 注册子区事件监听器，用于监听子区变化，如子区的创建和解散等。
         *
         * 你可以调用 {@link #RemoveThreadManagerDelegate} 移除不需要的监听器。
         *
         * @param threadManagerDelegate      要注册的子区事件监听器。
         *
         * \~english
         * Adds the message thread event listener, which listens for message thread changes, such as the message thread creation and destruction.
         *
         * You can call {@link #RemoveThreadManagerDelegate} to remove an unnecessary message thread event listener.
         *
         * @param threadManagerDelegate The message thread event listener to add.
         */
        public void AddThreadManagerDelegate(IChatThreadManagerDelegate threadManagerDelegate)
        {
            if (!CallbackManager.Instance().threadManagerListener.delegater.Contains(threadManagerDelegate))
            {
                CallbackManager.Instance().threadManagerListener.delegater.Add(threadManagerDelegate);
            }
        }

        /**
         * \~chinese
         * 移除子区事件监听器。
         *
         * 在利用 {@link #AddThreadManagerDelegate} 注册子区事件监听器后调用此方法。
         *
         * @param listener      要移除的子区事件监听器。
         *
         * \~english
         * Removes the message thread event listener.
         *
         * After a message thread event listener is added with {@link #AddThreadManagerDelegate}, you can call this method to remove it when it is not required.
         *
         * @param listener  The message thread event listener to remove.
         */
        public void RemoveThreadManagerDelegate(IChatThreadManagerDelegate threadManagerDelegate)
        {
            if (CallbackManager.IsQuit()) return;
            if (CallbackManager.Instance().threadManagerListener.delegater.Contains(threadManagerDelegate))
            {
                CallbackManager.Instance().threadManagerListener.delegater.Remove(threadManagerDelegate);
            }
        }

        internal void ClearDelegates()
        {
            CallbackManager.Instance().threadManagerListener.delegater.Clear();
        }
    }
}
