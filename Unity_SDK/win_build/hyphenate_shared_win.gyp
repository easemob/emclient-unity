{
    'includes': ['emclient-linux/easemob_lib_only_v14x.gyp'],

    'variables': {
      'hyphenate_library': 'shared_library',
      'default_config_type': 'Debug',
      'msvs_RuntimeLibrary': '0', #md:2, mdd:3, mt:0, mtd:1
      'emclient-linux-path':'emclient-linux',
      'emclient-unity-path':'emclient-unity',
    },

    'target_defaults': {
      'default_configuration': '<(default_config_type)',
      'defines': [
        'AGORACHAT_EXPORT',
        'SKIP_FPA',
      ],
    },
    'targets': [
        {
            'target_name': 'hyphenateCWrapper',
            'type': '<(hyphenate_library)',
            'defines': [
               'WIN32_LEAN_AND_MEAN',
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
            'conditions': [
            	['OS=="win" and <(is_x64)==1', {
	            	'include_dirs': [
	                #'<(emclient-linux-path)/3rd_party/platform/win/depends/openssl_1.1.1l-x64-static-md/include',
	                '<(emclient-linux-path)/3rd_party/platform/win/depends/boringssl-1.1.1g-x64-header/include',
	                '<(emclient-linux-path)/3rd_party/platform/win/depends/sqlcipher_4.4.3-x64-static-md/include',
	            	],
            	}],
            	['OS=="win" and <(is_x64)==0', {
            		'include_dirs': [
	                #'<(emclient-linux-path)/3rd_party/platform/win/depends32/openssl_1.1.1l-x86-static-md/include',
	                '<(emclient-linux-path)/3rd_party/platform/win/depends32/boringssl-1.1.1g-x86-header/include',
	                '<(emclient-linux-path)/3rd_party/platform/win/depends32/sqlcipher_4.4.3-x86-static-md/include',
	            	],
            	}],
            ],
            'sources': [
            # Add our source files
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
            '<(emclient-unity-path)/Unity_SDK/c_wrapper/userinfo_manager.cpp',
            '<(emclient-unity-path)/Unity_SDK/c_wrapper/presence_manager.cpp',
            '<(emclient-unity-path)/Unity_SDK/c_wrapper/thread_manager.cpp'
            ],
            'link_settings': {
             'conditions': [
                 ['OS=="win" and <(is_x64)==1', {
                      'libraries': [
                         #'libcrypto.lib',
                         #'libssl.lib',
                         'agora_fpa_sdk.lib',
                         'libcurl.lib',
                         'libsqlcipher.lib',
                         'zlib.lib',
                         'easemob.lib',
                         'crypt32.lib',
                         '%(AdditionalDependencies)',
                        ],
                       'library_dirs': [
                           '<(emclient-linux-path)/3rd_party/platform/win/depends/curl_7.80.0-x64-static-md/lib',
                           #'<(emclient-linux-path)/3rd_party/platform/win/depends/openssl_1.1.1l-x64-static-md/lib',
                           '<(emclient-linux-path)/3rd_party/platform/win/depends/fpa_1.2.0-x64-static-mt/lib',
                           '<(emclient-linux-path)/3rd_party/platform/win/depends/sqlite_3.34.1-x64-static-md/lib',
                           '<(emclient-linux-path)/3rd_party/platform/win/depends/zlib_1.2.11-x64-static-md/lib',
                           '<(emclient-linux-path)/3rd_party/platform/win/depends/sqlcipher_4.4.3-x64-static-md/lib',
                           '%(AddtionalLibrayDirectories)',
                       ],
                       'defines': [
                          'CURL_STATICLIB',
                       ],
                 }],
               	['OS=="win" and <(is_x64)==0', {
                      'libraries': [
                         #'libcrypto.lib',
                         #'libssl.lib',
                         'agora_fpa_sdk.lib',
                         'libcurl.lib',
                         'libsqlcipher.lib',
                         'zlib.lib',
                         'easemob.lib',
                         'crypt32.lib',
                         '%(AdditionalDependencies)',
                        ],
                       'library_dirs': [
                           '<(emclient-linux-path)/3rd_party/platform/win/depends32/curl_7.80.0-x86-static-md/lib',
                           #'<(emclient-linux-path)/3rd_party/platform/win/depends32/openssl_1.1.1l-x86-static-md/lib',
                           '<(emclient-linux-path)/3rd_party/platform/win/depends/fpa_1.2.0-x86-static-mt/lib',
                           '<(emclient-linux-path)/3rd_party/platform/win/depends32/zlib_1.2.11-x86-static-md/lib',
                           '<(emclient-linux-path)/3rd_party/platform/win/depends32/sqlcipher_4.4.3-x86-static-md/lib',
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
