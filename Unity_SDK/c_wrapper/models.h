#ifndef _MODELS_H_
#define _MODELS_H_
typedef struct _Options
{
  char *AppKey;
  char *DNSURL;
  char *IMServer;
  char *RestServer;
  bool DebugMode;
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
  int IMPort;

} Options;

#endif //_MODELS_H_
