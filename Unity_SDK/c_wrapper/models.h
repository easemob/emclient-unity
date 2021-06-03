#ifndef _MODELS_H_
#define _MODELS_H_
typedef struct _Options
{
  char *AppKey;
  bool DebugModel;
  bool AutoLogin;
  bool AcceptInvitationAlways;
  bool AutoAcceptGroupInvitation;
  bool RequireAck;
  bool RequireDeliveryAck;
  bool DeleteMessagesAsExitGroup;
  bool DeleteMessagesAsExitRoom;
  bool IsRoomOwnerLeaveAllowed;
  bool SortMessageByServerTime;
  bool UsingHttpsOnly;
  bool ServerTransfer;
  bool IsAutoDownload;
  bool EnableDNSConfig;
  char *RestServer;
  char *IMServer;
  int IMPort;
  char *DNSURL;
} Options;

#endif //_MODELS_H_
