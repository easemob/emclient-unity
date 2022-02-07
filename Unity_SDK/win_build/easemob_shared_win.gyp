{
    'variables': {
      'library': 'shared_library',
      'standalone': 1,
      'msvs_RuntimeLibrary': '3', #md:2, mdd:3, mt:0, mtd:1
      'emclient-linux-path':'emclient-linux',
      'emclient-unity-path':'emclient-unity'
    },

    'target_defaults': {
      'msvs_configuration_platform': 'x64',
      'default_configuration': 'Debug',
      'configurations': {
         'Debug':  {
             'cflags':[
                 '-g3',
             ],
             'defines': [
                  'DLL_EXPORT',
             ],
            'msbuild_toolset': 'v142',
            'msvs_settings': {
              'VCLinkerTool': {
                'GenerateDebugInformation': 'true',
                'OutputFile': '$(OutDir)$(ProjectName)$(TargetExt)',
                'ImportLibrary': '$(OutDir)$(TargetName).lib',
                'IgnoreDefaultLibraryNames': 'LIBCMTD.lib',       
              },
              'VCCLCompilerTool': {
                'Optimization': '0',
                'RuntimeLibrary': '<(msvs_RuntimeLibrary)',        
                'LanguageStandard': 'stdcpp17',
                'ProgramDataBaseFileName': './Debug/$(TargetName).pdb',
                'AdditionalOptions': [
                  '/utf-8',
                  '/D "RAPIDJSON_NAMESPACE"=easemob'
                  ]
              },
            },
            'msvs_configuration_attributes': {
              'CharacterSet': '1'
            },       
         },
         'Release': {
             'cflags': [
                # part of O2 and decrease size
                 '-Os',
             ],
             'defines': [
                  'DLL_EXPORT',
             ],
             'msbuild_toolset': 'v142',
             'msvs_settings': {
                'VCLinkerTool': {
                  'GenerateDebugInformation': 'true',
                  'OutputFile': '$(OutDir)$(ProjectName)$(TargetExt)',
                  'ImportLibrary': '$(OutDir)$(TargetName).lib',
                  'IgnoreDefaultLibraryNames': 'LIBCMTD.lib',                  
                  },
                  'VCCLCompilerTool': {
                    'Optimization': '0',
                    'RuntimeLibrary': '<(msvs_RuntimeLibrary)',                    
                    'LanguageStandard': 'stdcpp17',
                    'ProgramDataBaseFileName': './Release/$(TargetName).pdb',
                    'AdditionalOptions': [
                      '/utf-8',
                      '/D "RAPIDJSON_NAMESPACE"=easemob'
                      ]
                  },
              },
              'msvs_configuration_attributes': {
                'CharacterSet': '1'
              },
         },
      },
      'cflags': [
          '-Wall',
          '-fPIC',
      ],
      'cflags_cc': [
          '-Wall',
          '-std=c++11',
          '-fPIC',
      ],
      'defines':[
          '_WIN32',
          'AGORACHAT_EXPORT',
      ],
    },
    'targets': [
        {
            'target_name': 'hyphenateCWrapper',
            'type': '<(library)',
            'include_dirs': [
                '<(emclient-linux-path)/3rd_party/protobuf',
                #'<(emclient-linux-path)/3rd_party/platform/win/include',
                #'<(emclient-linux-path)/3rd_party/openssl/include',
                '<(emclient-linux-path)/3rd_party/curlcpp/include',
                #'<(emclient-linux-path)/3rd_party/sqlite3/include',
                '<(emclient-linux-path)/3rd_party/platform/win/depends/curl_7.40.0-x64-static-md/include',
                '<(emclient-linux-path)/3rd_party/platform/win/depends/openssl_1.0.2l-x64-static-md/include',
                '<(emclient-linux-path)/3rd_party/platform/win/depends/sqlite_3.34.1-x64-static-md/include',
                '<(emclient-linux-path)/3rd_party/rapidjson/include',
                '<(emclient-linux-path)/protocol/generated',
                '<(emclient-linux-path)/protocol',
                '<(emclient-linux-path)/include/message',
                '<(emclient-linux-path)/include/utils',
                '<(emclient-linux-path)/include',
            ],
            'sources': [
            # Add curlcpp in the same library
            '<(emclient-linux-path)/3rd_party/curlcpp/src/curl_easy.cpp',
            '<(emclient-linux-path)/3rd_party/curlcpp/src/curl_exception.cpp',
            '<(emclient-linux-path)/3rd_party/curlcpp/src/curl_form.cpp',
            '<(emclient-linux-path)/3rd_party/curlcpp/src/curl_header.cpp',
            '<(emclient-linux-path)/3rd_party/curlcpp/src/curl_writer.cpp',
            
            # Add protobuf
            '<(emclient-linux-path)/3rd_party/protobuf/google/protobuf/generated_message_util.cc',
            '<(emclient-linux-path)/3rd_party/protobuf/google/protobuf/message_lite.cc',
            '<(emclient-linux-path)/3rd_party/protobuf/google/protobuf/repeated_field.cc',
            '<(emclient-linux-path)/3rd_party/protobuf/google/protobuf/wire_format_lite.cc',
            '<(emclient-linux-path)/3rd_party/protobuf/google/protobuf/io/coded_stream.cc',
            '<(emclient-linux-path)/3rd_party/protobuf/google/protobuf/io/zero_copy_stream.cc',
            '<(emclient-linux-path)/3rd_party/protobuf/google/protobuf/io/zero_copy_stream_impl_lite.cc',
            '<(emclient-linux-path)/3rd_party/protobuf/google/protobuf/stubs/common.cc',
            '<(emclient-linux-path)/3rd_party/protobuf/google/protobuf/stubs/once.cc',
            '<(emclient-linux-path)/3rd_party/protobuf/google/protobuf/stubs/atomicops_internals_x86_msvc.cc',
            
            # Add our source files
            '<(emclient-linux-path)/protocol/basenode.cpp',
            '<(emclient-linux-path)/protocol/chatclient.cpp',
            '<(emclient-linux-path)/protocol/conference.cpp',
            '<(emclient-linux-path)/protocol/conferencebody.cpp',
            '<(emclient-linux-path)/protocol/emcompressionzlib.cpp',
            '<(emclient-linux-path)/protocol/emconnectionfactory.cpp',
            '<(emclient-linux-path)/protocol/emconnectiontcpbase.cpp',
            '<(emclient-linux-path)/protocol/emconnectiontcpclient.cpp',
            '<(emclient-linux-path)/protocol/emlogsink.cpp',
            '<(emclient-linux-path)/protocol/emmutex.cpp',
            '<(emclient-linux-path)/protocol/keyvalue.cpp',
            '<(emclient-linux-path)/protocol/message.cpp',
            '<(emclient-linux-path)/protocol/messagebody.cpp',
            '<(emclient-linux-path)/protocol/messagebodycontent.cpp',
            '<(emclient-linux-path)/protocol/messageconfig.cpp',
            '<(emclient-linux-path)/protocol/meta.cpp',
            '<(emclient-linux-path)/protocol/metaqueue.cpp',
            '<(emclient-linux-path)/protocol/msync.cpp',
            '<(emclient-linux-path)/protocol/mucbody.cpp',
            '<(emclient-linux-path)/protocol/mucmeta.cpp',
            '<(emclient-linux-path)/protocol/notice.cpp',
            '<(emclient-linux-path)/protocol/protocolparser.cpp',
            '<(emclient-linux-path)/protocol/provision.cpp',
            '<(emclient-linux-path)/protocol/rosterbody.cpp',
            '<(emclient-linux-path)/protocol/rostermeta.cpp',
            '<(emclient-linux-path)/protocol/statistics.cpp',
            '<(emclient-linux-path)/protocol/status.cpp',
            '<(emclient-linux-path)/protocol/syncdl.cpp',
            '<(emclient-linux-path)/protocol/syncul.cpp',
            '<(emclient-linux-path)/protocol/testserver.cpp',
            '<(emclient-linux-path)/protocol/uid.cpp',
            '<(emclient-linux-path)/protocol/unreaddl.cpp',
            '<(emclient-linux-path)/protocol/unreadul.cpp',
            '<(emclient-linux-path)/protocol/generated/conferencebody.pb.cc',
            '<(emclient-linux-path)/protocol/generated/jid.pb.cc',
            '<(emclient-linux-path)/protocol/generated/keyvalue.pb.cc',
            '<(emclient-linux-path)/protocol/generated/messagebody.pb.cc',
            '<(emclient-linux-path)/protocol/generated/msync.pb.cc',
            '<(emclient-linux-path)/protocol/generated/mucbody.pb.cc',
            '<(emclient-linux-path)/protocol/generated/rosterbody.pb.cc',
            '<(emclient-linux-path)/protocol/generated/statisticsbody.pb.cc',
            '<(emclient-linux-path)/src/b64decoder.c',
            '<(emclient-linux-path)/src/b64encoder.c',
            '<(emclient-linux-path)/src/emattributevalue.cpp',
            '<(emclient-linux-path)/src/emchatclient.cpp',
            '<(emclient-linux-path)/src/emchatclientimpl.cpp',
            '<(emclient-linux-path)/src/emchatconfigs.cpp',
            '<(emclient-linux-path)/src/emchatmanager.cpp',
            '<(emclient-linux-path)/src/emchatroom.cpp',
            '<(emclient-linux-path)/src/emchatroommanager.cpp',
            '<(emclient-linux-path)/src/emclient.cpp',
            '<(emclient-linux-path)/src/emcollector.cpp',
            '<(emclient-linux-path)/src/emconfigmanager.cpp',
            '<(emclient-linux-path)/src/emcontactmanager.cpp',
            '<(emclient-linux-path)/src/emconversation.cpp',
            '<(emclient-linux-path)/src/emconversationprivate.cpp',
            '<(emclient-linux-path)/src/emconversation_manager.cpp',
            '<(emclient-linux-path)/src/emdatabase.cpp',
            '<(emclient-linux-path)/src/emdatareport.cpp',
            '<(emclient-linux-path)/src/emdnsmanager.cpp',
            '<(emclient-linux-path)/src/emencryptproviderimpl.cpp',
            '<(emclient-linux-path)/src/emerror.cpp',
            '<(emclient-linux-path)/src/emgroup.cpp',
            '<(emclient-linux-path)/src/emgroupmanager.cpp',
            '<(emclient-linux-path)/src/emjsonstring.cpp',
            '<(emclient-linux-path)/src/emmuc.cpp',
            '<(emclient-linux-path)/src/emmucmanager.cpp',
            '<(emclient-linux-path)/src/empushconfigs.cpp',
            '<(emclient-linux-path)/src/empushmanager.cpp',
            '<(emclient-linux-path)/src/emreportevent.cpp',
            '<(emclient-linux-path)/src/emrtcconfigmanager.cpp',
            '<(emclient-linux-path)/src/emsemaphoretracker.cpp',
            '<(emclient-linux-path)/src/emsessionmanager.cpp',
            '<(emclient-linux-path)/src/emtaskqueue.cpp',
            '<(emclient-linux-path)/src/emuserinfomanager.cpp',
            '<(emclient-linux-path)/src/sqlite.cpp',
            
            '<(emclient-linux-path)/src/message/emcmdmessagebody.cpp',
            '<(emclient-linux-path)/src/message/emcustommessagebody.cpp',
            '<(emclient-linux-path)/src/message/emfilemessagebody.cpp',
            '<(emclient-linux-path)/src/message/emimagemessagebody.cpp',
            '<(emclient-linux-path)/src/message/emlocationmessagebody.cpp',
            '<(emclient-linux-path)/src/message/emmessage.cpp',
            '<(emclient-linux-path)/src/message/emmessagebody.cpp',
            '<(emclient-linux-path)/src/message/emmessageencoder.cpp',
            '<(emclient-linux-path)/src/message/emtextmessagebody.cpp',
            '<(emclient-linux-path)/src/message/emvideomessagebody.cpp',
            '<(emclient-linux-path)/src/message/emvoicemessagebody.cpp',
            
            '<(emclient-linux-path)/src/utils/emcryptoadapter.cpp',
            '<(emclient-linux-path)/src/utils/emencryptutils.cpp',
            '<(emclient-linux-path)/src/utils/emfilecompressor.cpp',
            '<(emclient-linux-path)/src/utils/emhttprequest.cpp',
            '<(emclient-linux-path)/src/utils/emlog.cpp',
            '<(emclient-linux-path)/src/utils/emtimer.cpp',
            '<(emclient-linux-path)/src/utils/emutils.cpp',
            
            '<(emclient-unity-path)/Unity_SDK/c_wrapper/chat_manager.cpp',
            '<(emclient-unity-path)/Unity_SDK/c_wrapper/client.cpp',
            '<(emclient-unity-path)/Unity_SDK/c_wrapper/contact_manager.cpp',
            '<(emclient-unity-path)/Unity_SDK/c_wrapper/conversation_manager.cpp',
            '<(emclient-unity-path)/Unity_SDK/c_wrapper/group_manager.cpp',
            '<(emclient-unity-path)/Unity_SDK/c_wrapper/hyphenateCWrapper.cpp',
            '<(emclient-unity-path)/Unity_SDK/c_wrapper/models.cpp',
            '<(emclient-unity-path)/Unity_SDK/c_wrapper/push_manager.cpp',
            '<(emclient-unity-path)/Unity_SDK/c_wrapper/room_manager.cpp',
            '<(emclient-unity-path)/Unity_SDK/c_wrapper/tool.cpp',
            '<(emclient-unity-path)/Unity_SDK/c_wrapper/userinfo_manager.cpp'
            ],
            'link_settings': {
               'conditions': [
                   ['OS=="win"', {
                       'libraries': [
                          'libcurl.lib',
                          'libeay32.lib',
                          'ssleay32.lib',
                          'libsqlite3.lib',
                          'zlib.lib',
                       ],
                       'library_dirs': [
                          '<(emclient-linux-path)/3rd_party/platform/win/depends/curl_7.40.0-x64-static-md/lib',
                          '<(emclient-linux-path)/3rd_party/platform/win/depends/openssl_1.0.2l-x64-static-md/lib',
                          '<(emclient-linux-path)/3rd_party/platform/win/depends/sqlite_3.34.1-x64-static-md/lib',
                          '<(emclient-linux-path)/3rd_party/platform/win/depends/zlib_1.2.11-x64-static-md/lib',
                          '%(AddtionalLibrayDirectories)',
                       ],
                       'defines': [
                           'CURL_STATICLIB',
                       ],
                   }],
                 ],
             },
        },
      ],
}
