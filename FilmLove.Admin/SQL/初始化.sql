
/****** Object:  Table [dbo].[web_sys_log]    Script Date: 2018/7/10 11:29:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[web_sys_log](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[manager_guid] [nvarchar](50) NOT NULL,
	[log_type] [int] NULL,
	[log_content] [nvarchar](4000) NULL,
	[log_time] [datetime] NULL,
	[log_name] [nvarchar](100) NULL,
	[manager_account] [nvarchar](50) NULL,
	[map_method] [nvarchar](50) NULL,
	[log_ip] [nvarchar](100) NULL,
 CONSTRAINT [PK_sys_log] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[web_sys_manager]    Script Date: 2018/7/10 11:29:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[web_sys_manager](
	[manager_id] [int] IDENTITY(1,1) NOT NULL,
	[manager_name] [nvarchar](30) NULL,
	[manager_pwd] [nvarchar](50) NULL,
	[manager_scal] [nvarchar](10) NULL,
	[manager_realname] [nvarchar](20) NULL,
	[manager_tel] [nvarchar](30) NULL,
	[manager_email] [nvarchar](50) NULL,
	[manager_isdel] [int] NULL,
	[manager_status] [int] NULL,
	[create_time] [datetime] NULL,
	[update_time] [datetime] NULL,
	[is_supper] [int] NULL,
	[last_login_time] [datetime] NULL,
	[cur_token] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[manager_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_web_sys_manager] UNIQUE NONCLUSTERED 
(
	[manager_name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[web_sys_manager_role]    Script Date: 2018/7/10 11:29:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[web_sys_manager_role](
	[auto_id] [int] IDENTITY(1,1) NOT NULL,
	[manager_id] [int] NULL,
	[role_id] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[auto_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[web_sys_menu]    Script Date: 2018/7/10 11:29:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[web_sys_menu](
	[menu_id] [int] IDENTITY(1,1) NOT NULL,
	[menu_name] [nvarchar](50) NULL,
	[menu_pid] [int] NULL,
	[menu_icon] [nvarchar](20) NULL,
	[index_code] [nvarchar](50) NULL,
	[menu_url] [nvarchar](200) NULL,
	[menu_status] [int] NULL,
	[menu_itempages] [nvarchar](3000) NULL,
	[create_time] [datetime] NULL,
	[update_time] [datetime] NULL,
	[menu_sort] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[menu_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_web_sys_menu] UNIQUE NONCLUSTERED 
(
	[index_code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[web_sys_menu_page]    Script Date: 2018/7/10 11:29:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[web_sys_menu_page](
	[page_id] [int] IDENTITY(1,1) NOT NULL,
	[menu_id] [int] NULL,
	[main_status] [int] NULL,
	[page_name] [nvarchar](50) NULL,
	[page_status] [int] NULL,
	[page_viewname] [nvarchar](50) NULL,
	[page_btnname] [nvarchar](50) NULL,
	[page_type] [int] NULL,
	[page_url] [nvarchar](300) NOT NULL,
	[page_paramters] [nvarchar](300) NULL,
	[create_time] [datetime] NULL,
	[update_time] [datetime] NULL,
 CONSTRAINT [PK_web_sys_MENU_PAGE] PRIMARY KEY CLUSTERED 
(
	[page_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[web_sys_role]    Script Date: 2018/7/10 11:29:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[web_sys_role](
	[role_id] [int] IDENTITY(1,1) NOT NULL,
	[role_name] [nvarchar](50) NULL,
	[role_status] [int] NULL,
	[create_time] [datetime] NULL,
	[update_time] [datetime] NULL,
	[role_remark] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[role_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[web_sys_role_menu]    Script Date: 2018/7/10 11:29:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[web_sys_role_menu](
	[auto_id] [int] IDENTITY(1,1) NOT NULL,
	[role_id] [int] NULL,
	[menu_id] [int] NULL,
	[page_ids] [nvarchar](300) NULL,
PRIMARY KEY CLUSTERED 
(
	[auto_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[web_sys_log] ON 

INSERT [dbo].[web_sys_log] ([id], [manager_guid], [log_type], [log_content], [log_time], [log_name], [manager_account], [map_method], [log_ip]) VALUES (1, N'1', 3, N'{"WhereParam":{"LoginName":"admin","VerCode":"9153"}}', CAST(0x0000A9000103E843 AS DateTime), N'登录', N'admin', N'WebEntrance/DoLogin', N'127.0.0.1')
SET IDENTITY_INSERT [dbo].[web_sys_log] OFF
SET IDENTITY_INSERT [dbo].[web_sys_manager] ON 

INSERT [dbo].[web_sys_manager] ([manager_id], [manager_name], [manager_pwd], [manager_scal], [manager_realname], [manager_tel], [manager_email], [manager_isdel], [manager_status], [create_time], [update_time], [is_supper], [last_login_time], [cur_token]) VALUES (1, N'admin', N'b7fd5c4dc06885adf17c1230532ef047', N'556024', N'管理员', N'13333333333', N'1222@qq.com', 0, 1, CAST(0x0000A6B900A24A7C AS DateTime), CAST(0x0000A8EE0108D1A5 AS DateTime), 1, CAST(0x0000A91900B9357C AS DateTime), N'9171b972005d28dd4c67b60cec5384db')
SET IDENTITY_INSERT [dbo].[web_sys_manager] OFF
SET IDENTITY_INSERT [dbo].[web_sys_menu] ON 

INSERT [dbo].[web_sys_menu] ([menu_id], [menu_name], [menu_pid], [menu_icon], [index_code], [menu_url], [menu_status], [menu_itempages], [create_time], [update_time], [menu_sort]) VALUES (1, N'系统管理', 0, N'cogs', N'00001', NULL, 1, N'', CAST(0x0000A85D011D0E52 AS DateTime), CAST(0x0000A8EE01189512 AS DateTime), 999999)
INSERT [dbo].[web_sys_menu] ([menu_id], [menu_name], [menu_pid], [menu_icon], [index_code], [menu_url], [menu_status], [menu_itempages], [create_time], [update_time], [menu_sort]) VALUES (2, N'管理员管理', 1, NULL, N'0000100002', N'/WebSystem/AccountList', 1, N'', CAST(0x0000A85D011D0E52 AS DateTime), CAST(0x0000A8EE00F5B17B AS DateTime), 0)
INSERT [dbo].[web_sys_menu] ([menu_id], [menu_name], [menu_pid], [menu_icon], [index_code], [menu_url], [menu_status], [menu_itempages], [create_time], [update_time], [menu_sort]) VALUES (3, N'后台菜单管理', 1, NULL, N'0000100003', N'/WebSystem/MenuList', 1, NULL, CAST(0x0000A85D011D0E52 AS DateTime), CAST(0x0000A85D011D0E52 AS DateTime), 0)
INSERT [dbo].[web_sys_menu] ([menu_id], [menu_name], [menu_pid], [menu_icon], [index_code], [menu_url], [menu_status], [menu_itempages], [create_time], [update_time], [menu_sort]) VALUES (4, N'后台角色管理', 1, NULL, N'0000100004', N'/WebSystem/RoleList', 1, NULL, CAST(0x0000A85D011D0E52 AS DateTime), CAST(0x0000A85D011D0E52 AS DateTime), 0)
SET IDENTITY_INSERT [dbo].[web_sys_menu] OFF
SET IDENTITY_INSERT [dbo].[web_sys_menu_page] ON 

INSERT [dbo].[web_sys_menu_page] ([page_id], [menu_id], [main_status], [page_name], [page_status], [page_viewname], [page_btnname], [page_type], [page_url], [page_paramters], [create_time], [update_time]) VALUES (1, 2, 1, N'人员管理', 1, N'人员管理', N'', 1, N'/WebSystem/AccountList', N'', CAST(0x0000A8E7010EF001 AS DateTime), CAST(0x0000A91B01389E07 AS DateTime))
INSERT [dbo].[web_sys_menu_page] ([page_id], [menu_id], [main_status], [page_name], [page_status], [page_viewname], [page_btnname], [page_type], [page_url], [page_paramters], [create_time], [update_time]) VALUES (2, 3, 1, N'后台菜单管理', 1, N'后台菜单管理', N'', 1, N'/WebSystem/MenuList', N'', CAST(0x0000A8E7010EF001 AS DateTime), CAST(0x0000A8E7010EF001 AS DateTime))
INSERT [dbo].[web_sys_menu_page] ([page_id], [menu_id], [main_status], [page_name], [page_status], [page_viewname], [page_btnname], [page_type], [page_url], [page_paramters], [create_time], [update_time]) VALUES (3, 4, 1, N'后台角色管理', 1, N'后台角色管理', N'', 1, N'/WebSystem/RoleList', N'', CAST(0x0000A8E7010EF001 AS DateTime), CAST(0x0000A8E7010EF001 AS DateTime))
INSERT [dbo].[web_sys_menu_page] ([page_id], [menu_id], [main_status], [page_name], [page_status], [page_viewname], [page_btnname], [page_type], [page_url], [page_paramters], [create_time], [update_time]) VALUES (7, 2, 0, N'管理员编辑页面', 1, N'编辑', N'新增管理员', 1, N'/WebSystem/AccountEdit', NULL, CAST(0x0000A8E701223135 AS DateTime), CAST(0x0000A8EE00ABFE0A AS DateTime))
INSERT [dbo].[web_sys_menu_page] ([page_id], [menu_id], [main_status], [page_name], [page_status], [page_viewname], [page_btnname], [page_type], [page_url], [page_paramters], [create_time], [update_time]) VALUES (8, 2, 0, N'管理员列表加载', 1, N'列表', N'列表加载', 2, N'/WebSystem/AccountLoadList', NULL, CAST(0x0000A8E7012350C6 AS DateTime), CAST(0x0000A8EB0120F320 AS DateTime))
INSERT [dbo].[web_sys_menu_page] ([page_id], [menu_id], [main_status], [page_name], [page_status], [page_viewname], [page_btnname], [page_type], [page_url], [page_paramters], [create_time], [update_time]) VALUES (9, 2, 0, N'管理员信息加载', 1, N'显示', N'信息加载', 2, N'/WebSystem/AccountLoadInfo', NULL, CAST(0x0000A8E70123773E AS DateTime), CAST(0x0000A8EB0120F9D4 AS DateTime))
INSERT [dbo].[web_sys_menu_page] ([page_id], [menu_id], [main_status], [page_name], [page_status], [page_viewname], [page_btnname], [page_type], [page_url], [page_paramters], [create_time], [update_time]) VALUES (10, 2, 0, N'管理员信息保存', 1, N'保存', N'保存', 2, N'/WebSystem/AccountSaveInfo', NULL, CAST(0x0000A8E701239F41 AS DateTime), CAST(0x0000A8E701239F41 AS DateTime))
INSERT [dbo].[web_sys_menu_page] ([page_id], [menu_id], [main_status], [page_name], [page_status], [page_viewname], [page_btnname], [page_type], [page_url], [page_paramters], [create_time], [update_time]) VALUES (11, 3, 0, N'菜单编辑页面', 1, N'编辑', N'新增菜单', 1, N'/WebSystem/MenuEdit', NULL, CAST(0x0000A8E7012539B0 AS DateTime), CAST(0x0000A95C01064798 AS DateTime))
INSERT [dbo].[web_sys_menu_page] ([page_id], [menu_id], [main_status], [page_name], [page_status], [page_viewname], [page_btnname], [page_type], [page_url], [page_paramters], [create_time], [update_time]) VALUES (12, 3, 0, N'子页面-列表页面', 1, N'子页面管理', N'子页面管理', 1, N'/WebSystem/MenuPageList', NULL, CAST(0x0000A8E70125A00E AS DateTime), CAST(0x0000A95C010C7635 AS DateTime))
INSERT [dbo].[web_sys_menu_page] ([page_id], [menu_id], [main_status], [page_name], [page_status], [page_viewname], [page_btnname], [page_type], [page_url], [page_paramters], [create_time], [update_time]) VALUES (13, 3, 0, N'子页面-列表数据', 1, N'列表数据', N'列表数据', 2, N'/WebSystem/MenuPageDataList', NULL, CAST(0x0000A8E70125B485 AS DateTime), CAST(0x0000A95C010C7CE8 AS DateTime))
INSERT [dbo].[web_sys_menu_page] ([page_id], [menu_id], [main_status], [page_name], [page_status], [page_viewname], [page_btnname], [page_type], [page_url], [page_paramters], [create_time], [update_time]) VALUES (14, 3, 0, N'子页面-编辑页面', 1, N'编辑', N'添加子页面', 1, N'/WebSystem/MenuPageEdit', NULL, CAST(0x0000A8E70125E007 AS DateTime), CAST(0x0000A95C010C85A9 AS DateTime))
INSERT [dbo].[web_sys_menu_page] ([page_id], [menu_id], [main_status], [page_name], [page_status], [page_viewname], [page_btnname], [page_type], [page_url], [page_paramters], [create_time], [update_time]) VALUES (15, 3, 0, N'子页面-编辑保存', 1, N'保存', N'保存', 2, N'/WebSystem/SaveMenuPage', NULL, CAST(0x0000A8E70126024B AS DateTime), CAST(0x0000A95C010CB4C0 AS DateTime))
INSERT [dbo].[web_sys_menu_page] ([page_id], [menu_id], [main_status], [page_name], [page_status], [page_viewname], [page_btnname], [page_type], [page_url], [page_paramters], [create_time], [update_time]) VALUES (16, 3, 0, N'菜单编辑加载', 1, N'菜单编辑加载', N'菜单编辑加载', 2, N'/WebSystem/MenuLoadInfo', NULL, CAST(0x0000A8E70126817A AS DateTime), CAST(0x0000A8E70126817A AS DateTime))
INSERT [dbo].[web_sys_menu_page] ([page_id], [menu_id], [main_status], [page_name], [page_status], [page_viewname], [page_btnname], [page_type], [page_url], [page_paramters], [create_time], [update_time]) VALUES (17, 3, 0, N'菜单列表数据', 1, N'菜单列表加载', N'菜单列表加载', 2, N'/WebSystem/MenuLoadList', NULL, CAST(0x0000A8E70126A513 AS DateTime), CAST(0x0000A95C010D2DC8 AS DateTime))
INSERT [dbo].[web_sys_menu_page] ([page_id], [menu_id], [main_status], [page_name], [page_status], [page_viewname], [page_btnname], [page_type], [page_url], [page_paramters], [create_time], [update_time]) VALUES (18, 3, 0, N'菜单编辑保存', 1, N'保存', N'保存', 2, N'/WebSystem/MenuSave', NULL, CAST(0x0000A8E70126C1ED AS DateTime), CAST(0x0000A919015F3B37 AS DateTime))
INSERT [dbo].[web_sys_menu_page] ([page_id], [menu_id], [main_status], [page_name], [page_status], [page_viewname], [page_btnname], [page_type], [page_url], [page_paramters], [create_time], [update_time]) VALUES (19, 4, 0, N'角色列表数据加载', 1, N'角色列表数据加载', N'角色列表数据加载', 2, N'/WebSystem/RoleLoadList', NULL, CAST(0x0000A8E70126E887 AS DateTime), CAST(0x0000A8E70126E887 AS DateTime))
INSERT [dbo].[web_sys_menu_page] ([page_id], [menu_id], [main_status], [page_name], [page_status], [page_viewname], [page_btnname], [page_type], [page_url], [page_paramters], [create_time], [update_time]) VALUES (20, 4, 0, N'角色编辑页面', 1, N'编辑', N'新增角色', 1, N'/WebSystem/RoleEdit', NULL, CAST(0x0000A8E70126FF4C AS DateTime), CAST(0x0000A8EE00B1D0D0 AS DateTime))
INSERT [dbo].[web_sys_menu_page] ([page_id], [menu_id], [main_status], [page_name], [page_status], [page_viewname], [page_btnname], [page_type], [page_url], [page_paramters], [create_time], [update_time]) VALUES (21, 4, 0, N'角色编辑页面加载', 1, N'角色编辑页面加载', N'角色编辑页面加载', 2, N'/WebSystem/RoleLoadInfo', NULL, CAST(0x0000A8E701271B74 AS DateTime), CAST(0x0000A8E701271B74 AS DateTime))
INSERT [dbo].[web_sys_menu_page] ([page_id], [menu_id], [main_status], [page_name], [page_status], [page_viewname], [page_btnname], [page_type], [page_url], [page_paramters], [create_time], [update_time]) VALUES (22, 4, 0, N'角色编辑页面保存', 1, N'保存', N'保存', 2, N'/WebSystem/RoleSave', NULL, CAST(0x0000A8E701272EE1 AS DateTime), CAST(0x0000A919016419C8 AS DateTime))
SET IDENTITY_INSERT [dbo].[web_sys_menu_page] OFF
SET ANSI_PADDING ON

GO

/****** Object:  Index [IX_web_sys_manager_role]    Script Date: 2018/7/10 11:29:07 ******/
ALTER TABLE [dbo].[web_sys_manager_role] ADD  CONSTRAINT [IX_web_sys_manager_role] UNIQUE NONCLUSTERED 
(
	[manager_id] ASC,
	[role_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

