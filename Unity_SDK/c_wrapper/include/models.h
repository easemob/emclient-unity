typedef struct _Options
{
  char *AppKey;
  unsigned char DebugModel;
  unsigned char AutoLogin;
  unsigned char AcceptInvitationAlways;
  unsigned char AutoAcceptGroupInvitation;
  unsigned char RequireAck;
  unsigned char RequireDeliveryAck;
  unsigned char DeleteMessagesAsExitGroup;
  unsigned char DeleteMessagesAsExitRoom;
  unsigned char IsRoomOwnerLeaveAllowed;
  unsigned char SortMessageByServerTime;
  unsigned char UsingHttpsOnly;
  unsigned char ServerTransfer;
  unsigned char IsAutoDownload;
  unsigned char EnableDNSConfig;
  char *RestServer;
  char *IMServer;
  int IMPort;
  char *DNSURL;
} Options;