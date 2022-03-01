{
    'variables': {
        'library': 'shared_library',
        'standalone': 0,
        'msvs_RuntimeLibrary': '3', #md:2, mdd:3, mt:0, mtd:1
        'ENABLE_CALL': '0',
        'USE_SQLCIPHER':'0',
        'emclient-linux-path':'../../../emclient-linux'
    },

    'target_defaults': {
       'default_configuration': 'Debug',
       'configurations': {
           'Debug':  {
               'cflags':[
                   '-g3',
                   '-fPIC',
               ],
               'defines': [
                    'DLL_EXPORT',
               ],
                'msbuild_toolset': 'v120_xp',
                'msvs_settings': {
                    'VCLibrarianTool': {
                        'AdditionalLibraryDirectories':'$(OutDir)lib;<(emclient-linux-path)/3rd_party/platform/win/lib;%(AdditionalLibraryDirectories)',
                        'OutputFile': '$(OutDir)$(ProjectName)_sd$(TargetExt)',
                        'AdditionalDependencies':'sqlite3.lib;libcurl.lib;libeay32MD.lib;libz.a',
                        'SubSystem': '2',
                        'MinimumRequiredVersion': '5.01'
                    },
                    'VCCLCompilerTool': {
                        'Optimization': '0',
                        'RuntimeLibrary': '3'
                    },
                },
                'msvs_configuration_attributes': {
                    'CharacterSet': '1'
                },
           },
           'Release': {
               'cflags': [
                   '-Os',
                   '-fPIC',
               ],
               'defines': [
                    'DLL_EXPORT',
               ],
                'msbuild_toolset': 'v120_xp',
                'msvs_settings': {
                    'VCLibrarianTool': {
                        'AdditionalLibraryDirectories':'$(OutDir)lib;<(emclient-linux-path)/3rd_party/platform/win/lib;%(AdditionalLibraryDirectories)',
                        'OutputFile': '$(OutDir)$(ProjectName)_s$(TargetExt)',
                        'AdditionalDependencies':'sqlite3.lib;libcurl.lib;libeay32MD.lib;libz.a',
                        'SubSystem': '2',
                        'MinimumRequiredVersion': '5.01'
                    },
                    'VCCLCompilerTool': {
                        'Optimization': '0',
                        'RuntimeLibrary': '0'
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
           # '-Os',
        ],
        'cflags_cc': [
            '-Wall',
          #  '-Os',
            '-std=c++11',
            '-fPIC',
        ],
        'xcode_settings': {
            'OTHER_CFLAGS' : ['<@(_cflags)'],
            'OTHER_CPLUSPLUSFLAGS' : ['<@(_cflags_cc)'],
            'CLANG_CXX_LANGUAGE_STANDARD': 'c++1y',
            'CLANG_CXX_LIBRARY': 'libc++',
            'CLANG_ENABLE_OBJC_ARC': 'YES',
            'MACOSX_DEPLOYMENT_TARGET': '10.8',
            'GCC_OPTIMIZATION_LEVEL': 0,
            'INFOPLIST_FILE': 'Info.plist',
            'INSTALL_PATH':'@rpath/',
            'EXCLUDED_ARCHS':'arm64'
        },
        'library_dirs': [
           '<(LIB_DIR)',
        ],
        'defines':[
           'RAPIDJSON_NAMESPACE=easemob'
        ],
    },
    'targets': [
        {
            'target_name': 'easemob',
            'mac_bundle': 1,
            'type': '<(library)',
            'include_dirs': [
                '<(emclient-linux-path)/include',
                '<(emclient-linux-path)/include/utils',
                '<(emclient-linux-path)/3rd_party/rapidjson/include',
                '<(emclient-linux-path)/3rd_party/curlcpp/include',
                '<(emclient-linux-path)/3rd_party/protobuf',
                #'<(emclient-linux-path)/3rd_party/openssl/include',
                '<(emclient-linux-path)/3rd_party/platform/darwin/depends/openssl_1.1.1l_share_intel/include',
                '<(emclient-linux-path)/3rd_party/platform/darwin/depends/curl_7.80.0_share_intel/include',
                '<(emclient-linux-path)/protocol',
                '<(emclient-linux-path)/protocol/generated',
                '<(emclient-linux-path)/3rd_party/md5',
            ],
            'standalone_static_library': '<(standalone)',
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
                # Add our source files
                '<(emclient-linux-path)/src/b64decoder.c',
                '<(emclient-linux-path)/src/b64encoder.c',
                '<(emclient-linux-path)/src/emclient.cpp',
                '<(emclient-linux-path)/src/emchatclient.cpp',
                '<(emclient-linux-path)/src/emchatclientimpl.cpp',
                '<(emclient-linux-path)/src/emchatmanager.cpp',
                '<(emclient-linux-path)/src/emcollector.cpp',
                '<(emclient-linux-path)/src/emconfigmanager.cpp',
                '<(emclient-linux-path)/src/emchatconfigs.cpp',
                '<(emclient-linux-path)/src/emcontactmanager.cpp',
                '<(emclient-linux-path)/src/emconversation.cpp',
                '<(emclient-linux-path)/src/emconversation_manager.cpp',
                '<(emclient-linux-path)/src/emconversationprivate.cpp',
                '<(emclient-linux-path)/src/emdatabase.cpp',
                '<(emclient-linux-path)/src/emdatareport.cpp',
                '<(emclient-linux-path)/src/emdnsmanager.cpp',
                '<(emclient-linux-path)/src/emrtcconfigmanager.cpp',
                '<(emclient-linux-path)/src/emreportevent.cpp',
                #'<(emclient-linux-path)/src/emencryptproviderimpl.cpp',
                '<(emclient-linux-path)/src/emerror.cpp',
                '<(emclient-linux-path)/src/emgroup.cpp',
                '<(emclient-linux-path)/src/emgroupmanager.cpp',
                '<(emclient-linux-path)/src/emsemaphoretracker.cpp',
                '<(emclient-linux-path)/src/emsessionmanager.cpp',
                '<(emclient-linux-path)/src/emtaskqueue.cpp',
                '<(emclient-linux-path)/src/emattributevalue.cpp',
                '<(emclient-linux-path)/src/emjsonstring.cpp',
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
                '<(emclient-linux-path)/src/sqlite.cpp',
                '<(emclient-linux-path)/src/utils/emhttprequest.cpp',
                '<(emclient-linux-path)/src/utils/emlog.cpp',
                '<(emclient-linux-path)/src/utils/emtimer.cpp',
                '<(emclient-linux-path)/src/utils/emutils.cpp',
                '<(emclient-linux-path)/src/utils/emfilecompressor.cpp',
                '<(emclient-linux-path)/src/utils/emencryptutils.cpp',
                '<(emclient-linux-path)/src/utils/emcryptoadapter.cpp',
                '<(emclient-linux-path)/src/emchatroom.cpp',
                '<(emclient-linux-path)/src/emchatroommanager.cpp',
                '<(emclient-linux-path)/src/emmuc.cpp',
                '<(emclient-linux-path)/src/emmucmanager.cpp',
                '<(emclient-linux-path)/src/empushconfigs.cpp',
                '<(emclient-linux-path)/src/empushmanager.cpp',
                '<(emclient-linux-path)/src/emuserinfomanager.cpp',
                # Add protocol source files
                '<(emclient-linux-path)/protocol/emconnectionfactory.cpp',
                '<(emclient-linux-path)/protocol/emconnectiontcpbase.cpp',
                '<(emclient-linux-path)/protocol/emconnectiontcpclient.cpp',
                '<(emclient-linux-path)/protocol/emmutex.cpp',
                '<(emclient-linux-path)/protocol/emlogsink.cpp',
                '<(emclient-linux-path)/protocol/chatclient.cpp',
                '<(emclient-linux-path)/protocol/uid.cpp',
                '<(emclient-linux-path)/protocol/keyvalue.cpp',
                '<(emclient-linux-path)/protocol/message.cpp',
                '<(emclient-linux-path)/protocol/messagebody.cpp',
                '<(emclient-linux-path)/protocol/messageconfig.cpp',
                '<(emclient-linux-path)/protocol/messagebodycontent.cpp',
                '<(emclient-linux-path)/protocol/rosterbody.cpp',
                '<(emclient-linux-path)/protocol/rostermeta.cpp',
                '<(emclient-linux-path)/protocol/metaqueue.cpp',
                '<(emclient-linux-path)/protocol/protocolparser.cpp',
                '<(emclient-linux-path)/protocol/msync.cpp',
                '<(emclient-linux-path)/protocol/notice.cpp',
                '<(emclient-linux-path)/protocol/status.cpp',
                '<(emclient-linux-path)/protocol/syncdl.cpp',
                '<(emclient-linux-path)/protocol/syncul.cpp',
                '<(emclient-linux-path)/protocol/unreaddl.cpp',
                '<(emclient-linux-path)/protocol/unreadul.cpp',
                '<(emclient-linux-path)/protocol/meta.cpp',
                '<(emclient-linux-path)/protocol/mucbody.cpp',
                '<(emclient-linux-path)/protocol/mucmeta.cpp',
                '<(emclient-linux-path)/protocol/conference.cpp',
                '<(emclient-linux-path)/protocol/conferencebody.cpp',
                '<(emclient-linux-path)/protocol/statistics.cpp',
                '<(emclient-linux-path)/protocol/testserver.cpp',
                '<(emclient-linux-path)/protocol/provision.cpp',
                '<(emclient-linux-path)/protocol/basenode.cpp',
                '<(emclient-linux-path)/protocol/emcompressionzlib.cpp',
                '<(emclient-linux-path)/protocol/generated/jid.pb.cc',
                '<(emclient-linux-path)/protocol/generated/keyvalue.pb.cc',
                '<(emclient-linux-path)/protocol/generated/messagebody.pb.cc',
                '<(emclient-linux-path)/protocol/generated/msync.pb.cc',
                '<(emclient-linux-path)/protocol/generated/mucbody.pb.cc',
                '<(emclient-linux-path)/protocol/generated/rosterbody.pb.cc',
                '<(emclient-linux-path)/protocol/generated/conferencebody.pb.cc',
                '<(emclient-linux-path)/protocol/generated/statisticsbody.pb.cc',
            ],
            #'mac_bundle_resources': [
            #     '<(emclient-linux-path)/3rd_party/platform/darwin/lib/libcrypto.1.0.0.dylib',
            #    '<(emclient-linux-path)/3rd_party/platform/darwin/lib/libcurl.4.dylib',
            #    '<(emclient-linux-path)/3rd_party/platform/darwin/lib/libssl.1.0.0.dylib',
            #    #'<(emclient-linux-path)/3rd_party/platform/darwin/lib/libz.dylib',
            #    # '<(emclient-linux-path)/3rd_party/platform/darwin/lib/libsqlite3.dylib',
            #],
            'ldflags': [
                # Needed by protocol
                '-pthread',
            ],
            'conditions': [
                ['"<!(python <(emclient-linux-path)/tools/cross_compile.py)"=="yes"', {
                    'include_dirs': [
                        #corss compile headers
                        '<(emclient-linux-path)/3rd_party/platform/raspberrypi/include/',
                        '<(emclient-linux-path)/3rd_party/platform/raspberrypi/include/curl',
                            ],
                }],
                ['USE_SQLCIPHER==1',{
                    'defines':[
                        'USE_SQLCIPHER=1'
                    ],
                }],
                ['USE_SQLCIPHER==1 and OS=="mac"',{
                    'link_settings': {
                        'libraries': [
                            'libsqlcipher.dylib',
                        ],
                    },
                }],
                ['USE_SQLCIPHER!=1 and OS=="mac"',{
                    'link_settings': {
                        'libraries': [
                            'libsqlite3.0.dylib',
                        ],
                        'library_dirs': [
                           #'<(emclient-linux-path)/3rd_party/platform/darwin/lib',
                           '<(emclient-linux-path)/3rd_party/platform/darwin/depends/sqlite_3.34.1_share_intel/lib',
                           #'/usr/local/lib',
                       ],
                    },
                }],
                ['OS=="win"', {'sources': [
                        '<(emclient-linux-path)/3rd_party/protobuf/google/protobuf/stubs/atomicops_internals_x86_msvc.cc',
                        '<(emclient-linux-path)/3rd_party/platform/win/src/sqlite3.c',
                    ],
                    'include_dirs': [
                        '<(emclient-linux-path)/3rd_party/platform/win/include',
                    ],
                    'defines': [
                        'CURL_STATICLIB',
                    ],
                }, {'sources': [
                        '<(emclient-linux-path)/3rd_party/protobuf/google/protobuf/stubs/atomicops_internals_x86_gcc.cc',
                    ],
                },],
            ],
            'link_settings': {
               'conditions': [
                   ['OS=="mac"', {
                       'libraries': [
                           #'libsqlcipher.dylib',
                           'libcurl.4.dylib',
                           'libz.1.2.11.dylib',
                           'libcrypto.1.1.dylib',
                           'libssl.1.1.dylib',
                       ],
                       'library_dirs': [
                           #'<(emclient-linux-path)/3rd_party/platform/darwin/lib',
                           # '/usr/local/lib',
                           '<(emclient-linux-path)/3rd_party/platform/darwin/depends/zlib_1.2.11_share_intel/lib',
                           '<(emclient-linux-path)/3rd_party/platform/darwin/depends/openssl_1.1.1l_share_intel/lib',
                           '<(emclient-linux-path)/3rd_party/platform/darwin/depends/curl_7.80.0_share_intel/lib',
                           '<(emclient-linux-path)/3rd_party/platform/darwin/depends/sqlite_3.34.1_share_intel/lib', 
                       ],
                   }],
                 ],
             },
             'direct_dependent_settings': {
                'include_dirs': [
                    '<(emclient-linux-path)/include',
                ],
            },
        },
      ],
}
