using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows;

[assembly: ThemeInfo(
    ResourceDictionaryLocation.None, //where theme specific resource dictionaries are located
                                     //(used if a resource is not found in the page,
                                     // or application resource dictionaries)
    ResourceDictionaryLocation.SourceAssembly //where the generic resource dictionary is located
                                              //(used if a resource is not found in the page,
                                              // app, or any theme specific resource dictionaries)
)]

#if DEBUG
[assembly: InternalsVisibleTo("Metrolib.Test")]
#else
[assembly: InternalsVisibleTo("Metrolib.Test,PublicKey=00240000048000009400000006020000002400005253413100040000010001006d873a2f2f5d54" +
	"280b91e8a2b6997fbe287f0631db99675716fbd9ded5ae79276ec77851fbe7be4e975bae1bc1d6" +
	"dcc76d4e00ab7dbba236f2c2e842310cc6b842ae0785afd969bf0b2fc79b5a902cf0e7278dbf33" +
	"00e9158b2693d209dfda4670b3ef8f660b7bc7be6028bcef1665f4aaaa8cc6851d36968210ea77" +
	"1db7ebdb")]
#endif