﻿using System.Runtime.InteropServices;

namespace PetLab.DAL.Helper {
	public static class AccessFileHelper {
		public static void ConnectShare(string shareName, string username, string password) {
			NETRESOURCE nr = new NETRESOURCE();
			nr.dwType = ResourceType.RESOURCETYPE_DISK;
			nr.lpLocalName = null;
			nr.lpRemoteName = shareName;
			nr.lpProvider = null;

			int result = WNetAddConnection2(nr, password, username, 0);
		}

		[DllImport("Mpr.dll", EntryPoint = "WNetAddConnection2", CallingConvention = CallingConvention.Winapi)]
		private static extern int WNetAddConnection2(NETRESOURCE lpNetResource, string lpPassword,
									  string lpUsername, System.UInt32 dwFlags);

		[StructLayout(LayoutKind.Sequential)]
		private class NETRESOURCE {
			public ResourceScope dwScope = 0;
			public ResourceType dwType = 0;
			public ResourceDisplayType dwDisplayType = 0;
			public ResourceUsage dwUsage = 0;
			public string lpLocalName = null;
			public string lpRemoteName = null;
			public string lpComment = null;
			public string lpProvider = null;
		};

		public enum ResourceScope {
			RESOURCE_CONNECTED = 1,
			RESOURCE_GLOBALNET,
			RESOURCE_REMEMBERED,
			RESOURCE_RECENT,
			RESOURCE_CONTEXT
		};

		public enum ResourceType {
			RESOURCETYPE_ANY,
			RESOURCETYPE_DISK,
			RESOURCETYPE_PRINT,
			RESOURCETYPE_RESERVED
		};

		public enum ResourceUsage {
			RESOURCEUSAGE_CONNECTABLE = 0x00000001,
			RESOURCEUSAGE_CONTAINER = 0x00000002,
			RESOURCEUSAGE_NOLOCALDEVICE = 0x00000004,
			RESOURCEUSAGE_SIBLING = 0x00000008,
			RESOURCEUSAGE_ATTACHED = 0x00000010,
			RESOURCEUSAGE_ALL = (RESOURCEUSAGE_CONNECTABLE | RESOURCEUSAGE_CONTAINER | RESOURCEUSAGE_ATTACHED),
		};

		public enum ResourceDisplayType {
			RESOURCEDISPLAYTYPE_GENERIC,
			RESOURCEDISPLAYTYPE_DOMAIN,
			RESOURCEDISPLAYTYPE_SERVER,
			RESOURCEDISPLAYTYPE_SHARE,
			RESOURCEDISPLAYTYPE_FILE,
			RESOURCEDISPLAYTYPE_GROUP,
			RESOURCEDISPLAYTYPE_NETWORK,
			RESOURCEDISPLAYTYPE_ROOT,
			RESOURCEDISPLAYTYPE_SHAREADMIN,
			RESOURCEDISPLAYTYPE_DIRECTORY,
			RESOURCEDISPLAYTYPE_TREE,
			RESOURCEDISPLAYTYPE_NDSCONTAINER
		};

	}
}
