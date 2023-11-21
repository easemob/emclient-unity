{
    'variables': {
			'static_library': 'static_library',
			'shared_library': 'shared_library',
			'standalone': 1,
			'msvs_RuntimeLibrary': '2', #md:2, mdd:3, mt:0, mtd:1
			'emclient-linux-path':'emclient-linux',
			'emclient-unity-path':'emclient-unity',
    },

    'target_defaults': {
       'default_configuration': 'Release',
       
       'configurations': {
       
						'Common': {
               'cflags': [
                  '-Os',
                  '-fPIC',
               ],
               'defines': [
                   'DLL_EXPORT',
                   'SKIP_FPA',
                   '_WIN32',
                   'CURL_STATICLIB',
               ],
                'msvs_configuration_platform': 'x64',
                'msbuild_toolset': 'v142',
                'msvs_settings': {
                    'VCLibrarianTool': {
                      'OutputFile': '$(OutDir)$(ProjectName)$(TargetExt)',
                    },
                    'VCLinkerTool': {
                        'GenerateDebugInformation': 'true',
                        'OutputFile': '$(OutDir)$(ProjectName)$(TargetExt)',
                        'ImportLibrary': '$(OutDir)$(TargetName).lib',
                        #'IgnoreDefaultLibraryNames': 'LIBCMT.lib',
                     },
                     'VCCLCompilerTool': {
                         'Optimization': '0',
                         'RuntimeLibrary': '<(msvs_RuntimeLibrary)',
                         'ProgramDataBaseFileName': '.\Release\$(TargetName).pdb',
                         'AdditionalOptions': [
                             '/utf-8',
                             '/D RAPIDJSON_NAMESPACE=easemob',
                             '/D USE_SQLCIPHER=1',
                             '/D _ITERATOR_DEBUG_LEVEL=0',
                         ]
                      },
                },
                'msvs_configuration_attributes': {
                  'CharacterSet': '1'
                },
           	}, 
           	      
       			'Release': {
       				 'inherit_from': ['Common'],
           	},
           	
           	'Debug': {
                'inherit_from': ['Common'],
                'defines': [
                   'DEBUG',
               	],            	
               	'msvs_settings': {
               		'VCCLCompilerTool': {
               			'ProgramDataBaseFileName': '.\Debug\$(TargetName).pdb',
               		},
               	}
            },
            
            'Debug_32': {
                'inherit_from': ['Common'],
                'msvs_configuration_platform': 'Win32',
                'defines': [
                   'DEBUG',
               	],
               	'msvs_settings': {
               		'VCCLCompilerTool': {
               			'ProgramDataBaseFileName': '.\Debug\$(TargetName).pdb',
               		},
               	},
            },
            
            'Release_32': {
                'inherit_from': ['Common'],
                'msvs_configuration_platform': 'Win32',
            }
       },
       
       'cflags': [
            '-Wall',
        ],
        'cflags_cc': [
            '-Wall',
            '-std=c++11',
            '-fPIC',
        ],
        'library_dirs': [
            '$(OutDir)',
        ],
        'xcode_settings': {
            'OTHER_CFLAGS' : ['<@(_cflags)'],
            'OTHER_CPLUSPLUSFLAGS' : ['<@(_cflags_cc)'],
            'CLANG_CXX_LANGUAGE_STANDARD': 'c++11',
            'CLANG_CXX_LIBRARY': 'libc++',
            'CLANG_ENABLE_OBJC_ARC': 'YES',
            'MACOSX_DEPLOYMENT_TARGET': '10.8',
            'GCC_OPTIMIZATION_LEVEL': 0,
        },
    },
    
    'targets': [
    {
            'target_name': 'ChatCWrapper',
            'type': '<(shared_library)',
            'defines': [
               'AGORACHAT_EXPORT',
            ],
            'include_dirs': [
                '<(emclient-unity-path)/AgoraChatSDK/CWrapper/CWrapper/include',
            ],
            
            'sources': [
            # Add our source files
            	'<(emclient-unity-path)/AgoraChatSDK/CWrapper/CWrapper/include/Api_decorator.h',
            	'<(emclient-unity-path)/AgoraChatSDK/CWrapper/CWrapper/include/CWrapper.h',
            	'<(emclient-unity-path)/AgoraChatSDK/CWrapper/CWrapper/include/CWrapper_import.h',
            	'<(emclient-unity-path)/AgoraChatSDK/CWrapper/CWrapper/src/CWrapper.cpp',
            ],         
            'link_settings': {
            	'libraries': [
            		'CommonWrapper.lib'
            	],
            },
    },
    
    {
            'target_name': 'CommonWrapper',
            'type': '<(shared_library)',
            'defines': [
               'COMMON_WRAPPER_EXPORT',
            ],
            'include_dirs': [
                '<(emclient-unity-path)/AgoraChatSDK/CWrapper/Wrapper/Wrapper_Common/common_wrapper',
                '<(emclient-unity-path)/AgoraChatSDK/CWrapper/Wrapper/Wrapper_Common/sdk_wrapper',
            ],
            
            'sources': [
            # Add our source files
            	'<(emclient-unity-path)/AgoraChatSDK/CWrapper/Wrapper/Wrapper_Common/common_wrapper/common_wrapper.h',
            	'<(emclient-unity-path)/AgoraChatSDK/CWrapper/Wrapper/Wrapper_Common/common_wrapper/common_wrapper_internal.h',
            	'<(emclient-unity-path)/AgoraChatSDK/CWrapper/Wrapper/Wrapper_Common/sdk_wrapper/sdk_wrapper.h',
            	'<(emclient-unity-path)/AgoraChatSDK/CWrapper/Wrapper/Wrapper_Common/common_wrapper/common_cwrapper.cpp',
            ],
            
            'link_settings': {
            	'libraries': [
            		'SdkWrapper.lib'
            	],
            },
    },
    
    
    {
            'target_name': 'SdkWrapper',
            'type': '<(shared_library)',
            'defines': [
               'SDK_WRAPPER_EXPORT',
               '_CRT_SECURE_NO_WARNINGS',
               'NOMINMAX',
            ],
            'include_dirs': [
                '<(emclient-linux-path)/protocol/generated',
                '<(emclient-linux-path)/protocol',
                '<(emclient-linux-path)/include/message',
                '<(emclient-linux-path)/include/utils',
                '<(emclient-linux-path)/include',
                '<(emclient-linux-path)/3rd_party/protobuf',
                '<(emclient-linux-path)/3rd_party/rapidjson/include',
            ],
            
            'configurations':{            
	          		'Release':{
	          			'msvs_target_platform': 'x64',
	          			'msvs_settings': {
	               		'VCLinkerTool': {
	               			'IgnoreDefaultLibraryNames': 'LIBCMT.lib',
	               		},
	               	},
		            	'include_dirs': [
		                #'<(emclient-linux-path)/3rd_party/platform/win/depends/openssl_1.1.1l-x64-static-md/include',
		                #'<(emclient-linux-path)/3rd_party/platform/win/depends/boringssl-1.1.1g-x64-header/include',
		                '<(emclient-linux-path)/3rd_party/platform/win/depends/boringssl-1.1.1f-x64-static-md/include',
		                '<(emclient-linux-path)/3rd_party/platform/win/depends/sqlcipher_4.4.3-x64-static-md-boringssl/include',
		            	],
	          		},
	          		'Debug':{
	          			'inherit_from': ['Release'],
	          			'msvs_settings': {
	               		'VCLinkerTool': {
	               			'IgnoreDefaultLibraryNames': 'LIBCMTD.lib',
	               		},
	               	},
	          		},
	          		'Release_32':{
	          			'msvs_target_platform': 'x86',
	          			'msvs_settings': {
	               		'VCLinkerTool': {
	               			'IgnoreDefaultLibraryNames': 'LIBCMT.lib',
	               		},
	               	},
	            		'include_dirs': [
		                #'<(emclient-linux-path)/3rd_party/platform/win/depends32/openssl_1.1.1l-x86-static-md/include',
		                #'<(emclient-linux-path)/3rd_party/platform/win/depends32/boringssl-1.1.1g-x86-header/include',
		                '<(emclient-linux-path)/3rd_party/platform/win/depends32/boringssl-1.1.1f-x86-static-md/include',
		                '<(emclient-linux-path)/3rd_party/platform/win/depends32/sqlcipher_4.4.3-x86-static-md-boringssl/include',
		            	],
	          		},
	          		'Debug_32':{
	          			'inherit_from': ['Release_32'],
	          			'msvs_settings': {
	               		'VCLinkerTool': {
	               			'IgnoreDefaultLibraryNames': 'LIBCMT.lib',
	               		},
	               	},
	          		}
          		},

            'sources': [
            # Add our source files
            	'<(emclient-unity-path)/AgoraChatSDK/CWrapper/Wrapper/Wrapper_Common/sdk_wrapper/callbacks.h',
            	'<(emclient-unity-path)/AgoraChatSDK/CWrapper/Wrapper/Wrapper_Common/sdk_wrapper/models.h',
            	'<(emclient-unity-path)/AgoraChatSDK/CWrapper/Wrapper/Wrapper_Common/sdk_wrapper/sdk_wrapper.h',
            	'<(emclient-unity-path)/AgoraChatSDK/CWrapper/Wrapper/Wrapper_Common/sdk_wrapper/sdk_wrapper_internal.h',
            	'<(emclient-unity-path)/AgoraChatSDK/CWrapper/Wrapper/Wrapper_Common/sdk_wrapper/tool.h',
            	'<(emclient-unity-path)/AgoraChatSDK/CWrapper/Wrapper/Wrapper_Common/sdk_wrapper/client.cpp',
            	'<(emclient-unity-path)/AgoraChatSDK/CWrapper/Wrapper/Wrapper_Common/sdk_wrapper/chat_manager.cpp',
            	'<(emclient-unity-path)/AgoraChatSDK/CWrapper/Wrapper/Wrapper_Common/sdk_wrapper/contact_manager.cpp',
            	'<(emclient-unity-path)/AgoraChatSDK/CWrapper/Wrapper/Wrapper_Common/sdk_wrapper/conversation_manager.cpp',
            	'<(emclient-unity-path)/AgoraChatSDK/CWrapper/Wrapper/Wrapper_Common/sdk_wrapper/group_manager.cpp',
            	'<(emclient-unity-path)/AgoraChatSDK/CWrapper/Wrapper/Wrapper_Common/sdk_wrapper/models.cpp',
            	'<(emclient-unity-path)/AgoraChatSDK/CWrapper/Wrapper/Wrapper_Common/sdk_wrapper/presence_manager.cpp',
            	'<(emclient-unity-path)/AgoraChatSDK/CWrapper/Wrapper/Wrapper_Common/sdk_wrapper/room_manager.cpp',
            	'<(emclient-unity-path)/AgoraChatSDK/CWrapper/Wrapper/Wrapper_Common/sdk_wrapper/tool.cpp',
            	'<(emclient-unity-path)/AgoraChatSDK/CWrapper/Wrapper/Wrapper_Common/sdk_wrapper/thread_manager.cpp',
            	'<(emclient-unity-path)/AgoraChatSDK/CWrapper/Wrapper/Wrapper_Common/sdk_wrapper/userinfo_manager.cpp',
            	'<(emclient-unity-path)/AgoraChatSDK/CWrapper/Wrapper/Wrapper_Common/sdk_wrapper/core_dump.h',
            	'<(emclient-unity-path)/AgoraChatSDK/CWrapper/Wrapper/Wrapper_Common/sdk_wrapper/core_dump.cpp',
            ],
            
            'link_settings': {
            	'libraries': [
                   'libcrypto.lib',
                   'libssl.lib',
                   'zlib.lib',
                   #'agora_fpa_sdk.lib',
                   'libcurl.lib',
                   'libsqlcipher.lib',
                   'easemob.lib',
                   'crypt32.lib',
                   '%(AdditionalDependencies)',
                  ],
                  
							'configurations':{            
	          		'Release':{
		            	    'library_dirs': [
                           '<(emclient-linux-path)/3rd_party/platform/win/depends/curl_7.80.0-x64-static-md-boringssl/lib',
                           #'<(emclient-linux-path)/3rd_party/platform/win/depends/openssl_1.1.1l-x64-static-md/lib',
                           '<(emclient-linux-path)/3rd_party/platform/win/depends/boringssl-1.1.1f-x64-static-md/lib',
                           #'<(emclient-linux-path)/3rd_party/platform/win/depends/fpa_1.2.0-x64-static-mt/lib',
                           '<(emclient-linux-path)/3rd_party/platform/win/depends/sqlite_3.34.1-x64-static-md/lib',
                           '<(emclient-linux-path)/3rd_party/platform/win/depends/zlib_1.2.11-x64-static-md/lib',
                           #'<(emclient-linux-path)/3rd_party/platform/win/depends/sqlcipher_4.4.3-x64-static-md/lib',
                           '<(emclient-linux-path)/3rd_party/platform/win/depends/sqlcipher_4.4.3-x64-static-md-boringssl/lib',
                           '%(AddtionalLibrayDirectories)',
                      ],
	          		},
	          		'Debug':{
	          			'inherit_from': ['Release'],
	          		},
	          		'Release_32':{
	            			'library_dirs': [
                           '<(emclient-linux-path)/3rd_party/platform/win/depends32/boringssl-1.1.1f-x86-static-md/lib',
                           '<(emclient-linux-path)/3rd_party/platform/win/depends32/curl_7.80.0-x86-static-md-boringssl/lib',
                           #'<(emclient-linux-path)/3rd_party/platform/win/depends32/openssl_1.1.1l-x86-static-md/lib',
                           #'<(emclient-linux-path)/3rd_party/platform/win/depends32/fpa_1.2.0-x86-static-mt/lib',
                           '<(emclient-linux-path)/3rd_party/platform/win/depends32/zlib_1.2.11-x86-static-md/lib',
                           '<(emclient-linux-path)/3rd_party/platform/win/depends32/sqlcipher_4.4.3-x86-static-md-boringssl/lib',
                           '%(AddtionalLibrayDirectories)',
                  	],
	          		},
	          		'Debug_32':{
	          			'inherit_from': ['Release_32'],
	          		}
          		},
           },
        },
        
        {
            'target_name': 'easemob',
            'type': '<(static_library)',
            'include_dirs': [
                '<(emclient-linux-path)/3rd_party/protobuf',
                '<(emclient-linux-path)/3rd_party/ap/include',
                '<(emclient-linux-path)/3rd_party/fpa/include',
                '<(emclient-linux-path)/3rd_party/curlcpp/include',
                '<(emclient-linux-path)/3rd_party/rapidjson/include',
                '<(emclient-linux-path)/protocol/generated',
                '<(emclient-linux-path)/protocol',
                '<(emclient-linux-path)/include/message',
                '<(emclient-linux-path)/include/utils',
                '<(emclient-linux-path)/include',
            ],
            
            'configurations':{            
	          		'Release':{
	          			'msvs_target_platform': 'x64',
		            	'include_dirs': [
                        #'<(emclient-linux-path)/3rd_party/platform/win/depends/openssl_1.1.1l-x64-static-md/include',
                        #'<(emclient-linux-path)/3rd_party/platform/win/depends/boringssl-1.1.1g-x64-header/include',
                        '<(emclient-linux-path)/3rd_party/platform/win/depends/boringssl-1.1.1f-x64-static-md/include',
                        '<(emclient-linux-path)/3rd_party/platform/win/depends/curl_7.80.0-x64-static-md-boringssl/include',
                        '<(emclient-linux-path)/3rd_party/platform/win/depends/sqlcipher_4.4.3-x64-static-md-boringssl/include',
		            	],
	          		},
	          		'Debug':{
	          			'inherit_from': ['Release'],
	          		},
	          		'Release_32':{
	          			'msvs_target_platform': 'x86',
	            		'include_dirs': [
			                	#'<(emclient-linux-path)/3rd_party/platform/win/depends32/openssl_1.1.1l-x86-static-md/include',
	                      #'<(emclient-linux-path)/3rd_party/platform/win/depends32/boringssl-1.1.1g-x86-header/include',
	                      '<(emclient-linux-path)/3rd_party/platform/win/depends32/boringssl-1.1.1f-x86-static-md/include',
	                      '<(emclient-linux-path)/3rd_party/platform/win/depends32/curl_7.80.0-x86-static-md-boringssl/include',
	                      '<(emclient-linux-path)/3rd_party/platform/win/depends32/sqlcipher_4.4.3-x86-static-md-boringssl/include',
		            	],
	          		},
	          		'Debug_32':{
	          			'inherit_from': ['Release_32'],
	          		}
          		},

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
                '<(emclient-linux-path)/protocol/roamconfig.cpp',
                '<(emclient-linux-path)/protocol/muc.cpp',

                '<(emclient-linux-path)/protocol/generated/conferencebody.pb.cc',
                '<(emclient-linux-path)/protocol/generated/jid.pb.cc',
                '<(emclient-linux-path)/protocol/generated/keyvalue.pb.cc',
                '<(emclient-linux-path)/protocol/generated/messagebody.pb.cc',
                '<(emclient-linux-path)/protocol/generated/msync.pb.cc',
                '<(emclient-linux-path)/protocol/generated/mucbody.pb.cc',
                '<(emclient-linux-path)/protocol/generated/rosterbody.pb.cc',
                '<(emclient-linux-path)/protocol/generated/statisticsbody.pb.cc',
                '<(emclient-linux-path)/protocol/generated/argusdata.pb.cc',
                '<(emclient-linux-path)/protocol/generated/argussdkapi.pb.cc',
                '<(emclient-linux-path)/protocol/generated/argussdkinit.pb.cc',

                '<(emclient-linux-path)/src/b64decoder.c',
                '<(emclient-linux-path)/src/b64encoder.c',
                '<(emclient-linux-path)/src/emfpamanager.cpp',
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
                '<(emclient-linux-path)/src/emtranslatemanager.cpp',
                '<(emclient-linux-path)/src/emtranslateresult.cpp',
                '<(emclient-linux-path)/src/emmessagereactionchange.cpp',
                '<(emclient-linux-path)/src/empresence.cpp',
                '<(emclient-linux-path)/src/empresencemanager.cpp',
                '<(emclient-linux-path)/src/emreactionmanager.cpp',
                '<(emclient-linux-path)/src/emreportservice.cpp',
                '<(emclient-linux-path)/src/emrequestreport.cpp',
                '<(emclient-linux-path)/src/emthread.cpp',
                '<(emclient-linux-path)/src/emthreadevent.cpp',
                '<(emclient-linux-path)/src/emthreadmanager.cpp',
                '<(emclient-linux-path)/src/emhttprequestmonitor.cpp',
                '<(emclient-linux-path)/src/emmessagestatistics.cpp',
                '<(emclient-linux-path)/src/emstatisticsmanager.cpp',
                '<(emclient-linux-path)/src/emmessagecollect.cpp',
                '<(emclient-linux-path)/src/emfetchmessageoption.cpp',
                '<(emclient-linux-path)/src/emgroupprivate.cpp',
                '<(emclient-linux-path)/src/emuploadinparts.cpp',
            
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
                '<(emclient-linux-path)/src/message/emmessagereaction.cpp',
                '<(emclient-linux-path)/src/message/emcombinemessagebody.cpp',
            
                '<(emclient-linux-path)/src/utils/emcryptoadapter.cpp',
                '<(emclient-linux-path)/src/utils/emencryptutils.cpp',
                '<(emclient-linux-path)/src/utils/emfilecompressor.cpp',
                '<(emclient-linux-path)/src/utils/emhttprequest.cpp',
                '<(emclient-linux-path)/src/utils/emlog.cpp',
                '<(emclient-linux-path)/src/utils/emtimer.cpp',
                '<(emclient-linux-path)/src/utils/emutils.cpp',
                '<(emclient-linux-path)/src/utils/emaestool.cpp',
                
                '<(emclient-linux-path)/3rd_party/protobuf/google/protobuf/stubs/atomicops_internals_x86_msvc.cc',
            ],            
        },
      ],
}
