package com.hyphenate.wrapper;

import com.hyphenate.chat.EMConversation;

public class EMMode {
    public static EMConversation.EMMessageSearchScope searchScopeFromInt(int intType) {
        if (intType == 0){
            return EMConversation.EMMessageSearchScope.CONTENT;
        }else if(intType == 1){
            return EMConversation.EMMessageSearchScope.EXT;
        }else if(intType == 2){
            return EMConversation.EMMessageSearchScope.ALL;
        }else {
            return EMConversation.EMMessageSearchScope.CONTENT;
        }
    }

    public static EMConversation.EMMarkType markTypeFromInt(int intType) {
        switch(intType) {
            case 0:
                return EMConversation.EMMarkType.MARK_0;
            case 1:
                return EMConversation.EMMarkType.MARK_1;
            case 2:
                return EMConversation.EMMarkType.MARK_2;
            case 3:
                return EMConversation.EMMarkType.MARK_3;
            case 4:
                return EMConversation.EMMarkType.MARK_4;
            case 5:
                return EMConversation.EMMarkType.MARK_5;
            case 6:
                return EMConversation.EMMarkType.MARK_6;
            case 7:
                return EMConversation.EMMarkType.MARK_7;
            case 8:
                return EMConversation.EMMarkType.MARK_8;
            case 9:
                return EMConversation.EMMarkType.MARK_9;
            case 10:
                return EMConversation.EMMarkType.MARK_10;
            case 11:
                return EMConversation.EMMarkType.MARK_11;
            case 12:
                return EMConversation.EMMarkType.MARK_12;
            case 13:
                return EMConversation.EMMarkType.MARK_13;
            case 14:
                return EMConversation.EMMarkType.MARK_14;
            case 15:
                return EMConversation.EMMarkType.MARK_15;
            case 16:
                return EMConversation.EMMarkType.MARK_16;
            case 17:
                return EMConversation.EMMarkType.MARK_17;
            case 18:
                return EMConversation.EMMarkType.MARK_18;
            case 19:
                return EMConversation.EMMarkType.MARK_19;
            default:
                return EMConversation.EMMarkType.MARK_0;
        }
    }

    public static int intFromMarkType(EMConversation.EMMarkType markType) {
        switch(markType) {
            case MARK_0:
                return 0;
            case MARK_1:
                return 1;
            case MARK_2:
                return 2;
            case MARK_3:
                return 3;
            case MARK_4:
                return 4;
            case MARK_5:
                return 5;
            case MARK_6:
                return 6;
            case MARK_7:
                return 7;
            case MARK_8:
                return 8;
            case MARK_9:
                return 9;
            case MARK_10:
                return 10;
            case MARK_11:
                return 11;
            case MARK_12:
                return 12;
            case MARK_13:
                return 13;
            case MARK_14:
                return 14;
            case MARK_15:
                return 15;
            case MARK_16:
                return 16;
            case MARK_17:
                return 17;
            case MARK_18:
                return 18;
            case MARK_19:
                return 19;
            default:
                return 0;
        }
    }
}
