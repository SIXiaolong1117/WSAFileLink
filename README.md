<p align="center">
  <h1 align="center">WSA File Link</h1>
  <p align="center">个人的一个 C# + WinUI3 的练手项目，可以实现便捷的将文件传输到 WSA。</p>
  <p align="center">
    <a href="https://github.com/Direct5dom/WSAFileLink/blob/master/LICENSE">
      <img src="https://img.shields.io/github/license/Direct5dom/WSAFileLink"/>
    </a>
    <a href="https://github.com/Direct5dom/WSAFileLink/releases">
      <img src="https://img.shields.io/github/v/release/Direct5dom/WSAFileLink?display_name=tag"/>
    </a>
  </p>
  <p align="center">
    <a href="https://twitter.com/SI_Xiaolong">
      <img src="https://img.shields.io/badge/follow-SI_Xiaolong-blue?style=flat&logo=Twitter"/>
    </a>
  </p>
</p>

## 项目预览

<img src="./README/屏幕截图 2023-02-12 024855.png" width="50%"/>

<img src="./README/屏幕截图 2023-02-12 024903.png" width="50%"/>

## ⬇下载

您可以直接到 [Releases · Direct5dom/WSAFileLink](https://github.com/Direct5dom/WSAFileLink/releases) 下载我已经打包好的安装包。

安装需要注意的是，要右键“使用PowrShell”运行`Install.ps1`，而不是直接双击`WSAFileLink.msix`。

## ✋使用

### 准备和配置 ADB

本项目依赖 ADB (Android 调试桥) ，ADB 并不是本项目的一部分，需要您自行到 [Android 开源项目网站](https://source.android.google.cn/docs/setup/build/adb?hl=zh-cn#download-adb) 下载，出于精简的目的，您应该下载“独立的 SDK 平台工具”。

下载好的“独立的 SDK 平台工具”最好添加到系统的环境变量中，当然您也可以选择修改 WSA File Link 设置页面的 ADB 路径来指向 `adb.exe` 所在位置。

### 准备和配置 WSA

您首先需要确保 WSA 处于开机状态，最简单的做法就是让任意 WSA APP 运行在前台。

其次，您需要在 **适用于Android™ 的 Windows 子系统** 中，找到 **开发人员模式** 并打开该选项。

### 其他注意事项

在第一次尝试 **推送** (Push) 或 **拉取** (Pull) 时，会弹出一个窗口提示是否允许 ADB 连接，要点击“允许”才能让应用正常运行。

## 🛠️获取源码

要构建此项目，您需要将项目源码克隆到本地。

您可以使用 Git 命令行：

```powershell
git clone https://github.com/Direct5dom/WSAFileLink.git
```

或者更方便的，使用 Visual Studio 的“克隆存储库”克隆本仓库。

使用 Visual Studio 打开项目根目录的 `WSAFileLink.sln`，即可进行调试和打包。