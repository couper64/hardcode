# hardcode

## Command to recursively delete files.

    find . -type f -name '*.o' -exec rm {} +
    
## Command to recusrively clone a repository.

    git clone --recurse-submodules git://github.com/foo/bar.git

## Table of Content
* [Introduction](#introduction)
* [Section 1. Operating Systems](#section-1-operating-systems)
* [Section 1.1. Microsoft Operating Systems](#section-11-microsoft-operating-systems)
* [Section 1.2. Linux Operating Systems](#section-12-linux-operating-systems)
* [Section 1.3. Apple Operating Systems](#section-13-apple-operating-systems)

## Introduction

<p align="center">
  <img src="https://c.tenor.com/IErQHBRt6GIAAAAM/leonardo-dicaprio.gif" />
</p>

This is a repository that contains useful code snippets, including personal and internet code. I am trying to keep the repository somewhat structured, however, there is no single rigid structure by design, but rather a chaotic collection of things that I think might be interesting in the future. Enjoy figuring this out.

### Computer Science

<p align="center">
  <img src="https://media0.giphy.com/media/v1.Y2lkPTc5MGI3NjExaTVqeTR3dmk3MmkyN2RlZmNqZDY2MHp6ZWI2NzQ3NDRlcmZ6dHBhbyZlcD12MV9pbnRlcm5hbF9naWZfYnlfaWQmY3Q9Zw/scZPhLqaVOM1qG4lT9/giphy.webp" />
</p>

Computer science is the study of computers and computational systems, encompassing both theoretical foundations and practical applications. It involves the design and analysis of algorithms, data structures, software, and hardware to solve problems efficiently. Key areas include artificial intelligence, programming, data science, cybersecurity, and human-computer interaction. Computer science also explores how information is processed, stored, and communicated, along with the creation of innovative technologies that impact various fields such as medicine, engineering, and entertainment.

### Games Development

<p align="center">
  <img src="https://media.tenor.com/qJ_zQG8v-jMAAAAM/soa-game-dev.gif" />
</p>

Games development is the process of designing, creating, and programming interactive digital experiences, often referred to as video games. It involves a multidisciplinary approach, combining elements of storytelling, art, sound design, and programming to build engaging gameplay and immersive environments. Game development typically includes several stages, such as concept design, coding, asset creation, testing, and optimization for different platforms. Developers use game engines, tools, and technologies to bring concepts to life, catering to a wide range of platforms from consoles and PCs to mobile devices and virtual reality systems.

### Artificial Intelligence

<p align="center">
  <img src="https://media1.giphy.com/media/v1.Y2lkPTc5MGI3NjExaTE4dDZ4OThhdWxseHJ5aHByNnFjcXdvaWhpaHlhNXl4NjJrb3VvZCZlcD12MV9pbnRlcm5hbF9naWZfYnlfaWQmY3Q9Zw/xT9C25UNTwfZuk85WP/giphy.webp" />
</p>

Artificial intelligence (AI) is a branch of computer science focused on creating systems and machines that can simulate human intelligence. It involves the development of algorithms and models that allow computers to perform tasks such as learning from data, recognizing patterns, making decisions, solving problems, and understanding natural language. AI encompasses various subfields, including machine learning, neural networks, robotics, and natural language processing. Its applications span industries like healthcare, finance, transportation, and entertainment, enabling innovations such as autonomous vehicles, recommendation systems, and intelligent virtual assistants.

## Section 1. Operating Systems

<p align="center">
  <img src="https://media.tenor.com/sSYnkkUnFyIAAAAM/error-fail.gif" />
</p>

An operating system (OS) is a fundamental software layer that manages computer hardware and software resources, providing a user interface and acting as a bridge between the user and the computer's hardware. It controls basic functions such as file management, memory allocation, task scheduling, and device management, allowing different programs and applications to run efficiently. Key components of an OS include the kernel, which handles core operations, and utilities that provide a user-friendly environment. Operating systems are essential for running devices such as computers, smartphones, and servers, with examples including Windows, macOS, Linux, and Android.

### Section 1.1 Microsoft Operating Systems

Microsoft Windows is a product line of proprietary graphical operating systems developed and marketed by Microsoft. It is grouped into families and sub-families that cater to particular sectors of the computing industry – Windows (unqualified) for a consumer or corporate workstation, Windows Server for a server and Windows IoT for an embedded system. Defunct families include Windows 9x, Windows Mobile, Windows Phone, and Windows Embedded Compact.

---

1. **Windows 10**

    Windows 10 is a major release of Microsoft's Windows NT operating system. It is the direct successor to Windows 8.1, which was released nearly two years earlier. It was released to manufacturing on July 15, 2015, and later to retail on July 29, 2015. Windows 10 was made available for download via MSDN and TechNet, as a free upgrade for retail copies of Windows 8 and Windows 8.1 users via the Microsoft Store, and to Windows 7 users via Windows Update. Windows 10 receives new builds on an ongoing basis, which are available at no additional cost to users, in addition to additional test builds of Windows 10, which are available to Windows Insiders. Devices in enterprise environments can receive these updates at a slower pace, or use long-term support milestones that only receive critical updates, such as security patches, over their ten-year lifespan of extended support. In June 2021, Microsoft announced that support for Windows 10 editions which are not in the Long-Term Servicing Channel (LTSC) will end on October 14, 2025.

    * To display Russian characters instead of squares.
        1. Open Language settings.
        1. Open Administrative language settings.
        1. Open Language for non-Unicode programs.
        1. Click on the "Change system locale..." button.
        1. Under Current system locale, select Russian (Russia).
            1. Don't check "Beta: Use Unicode UTF-8 for worldwide language support".
        1. Restart the PC.
    * List of common applications I need.
        * 7-zip
        * miniconda3 (more details [here](https://github.com/couper64/hardcode.internal))
        * FastStone Image Viewer
        * Git
        * KeePass
          * Substituted KeePass with __KeePassX__ because it is more modern.
        * VLC
        * WSL (more details [here](https://github.com/couper64/hardcode.internal))
        * VirtualBox required Microsoft Redistributable 2019
        * Docker (more details [here](https://github.com/couper64/hardcode.internal))

### Section 1.2 Linux Operating Systems

---

#### Ubuntu
From Wikipedia: Ubuntu (/ʊˈbʊntuː/ (listen) uu-BUUN-too) is a Linux distribution based on Debian and composed mostly of free and open-source software. Ubuntu is officially released in three editions: Desktop, Server, and Core for Internet of things devices and robots. All of the editions can run on a computer alone, or in a virtual machine. Ubuntu is a popular operating system for cloud computing, with support for OpenStack.Ubuntu's default desktop changed back from the in-house Unity to GNOME after nearly 6.5 years in 2017 upon the release of version 17.10.

1. **Ubuntu 20.04.1**

    I was working with the 20.04.1 version. However, the Ubuntu Download page has updated their [*.iso] to the 20.04.5 version. Therefore, it is important to keep a copy of the installation file with the __20.04.1__ version.

    1. The target __kernel__ version is *5.15.0-x*, where x is a number of a patch.
    1. The target __NVIDIA driver__ version is *515.xx.yy*, where xx is a number of a minor version, and yy is a number of a patch.

1. **Ubuntu 20.04.5**

    The default installation file of Ubuntu 20.04 has been updated to 20.04.5. I decided to move to a new version and do an experiment. I will install the software and won't be updating the default packages. Luckily, the defualt kernel is __5.14.0-1051-oem__ and NVIDIA driver is __515.65.01__.

##### Ubuntu Standard Installation Routine
The standard installation involves the programs which are used regardless of 
a purpose of the installation, be it computer vision task or game development.
The programs listed in this section would be useful in any case.

The __build-essential__ package installs programs like compiler, linker, 
libraries, and headers which are used during the compilation of various 
software. Many third-party software uses it implicitly and would produce 
errors otherwise. It is often comes pre-installed, for example Ubuntu
20.04.5 has is out-of-the-box.

    sudo apt install build-essential

The __ubuntu-restricted-extras__ package installs programs which are not 
permitted in the installation ISO (Ubuntu) due to copyright issues. It installs
programs such as _ttf-mscorefonts-installer_, _libavcodec-extra_, 
_libavcodec-extra58_, _libmspack0_, _libvo-amrwbenc0_, _unrar_, _cabextract_,
_libaribb24-0_

    sudo apt install ubuntu-restricted-extras

The __gstreamer1.0-libav__, __gstreamer1.0-plugins-ugly__, 
__gstreamer1.0-vaapi__ packages install programs which are not permitted in the
installation ISO due to copyright issues. It is not different from the
__ubuntu-restricted-extras__ package, however, the __ubuntu-restricted-extras__
package is available only on Ubuntu. In cases when I install different Linux,
I can use the following command.

    sudo apt install gstreamer1.0-libav \
                     gstreamer1.0-plugins-ugly \
                     gstreamer1.0-vaapi

The __fonts-powerline__ package installs special fonts which are used to make 
BASH easier on eye, i.e. oh-my-bash.

    sudo apt install fonts-powerline

The __p7zip-full__ package install a utility to archive & unarchive files. I think that's important. Little reminder: _7z x "archive.zip"_ _-ooutput/path_ hasn't any space between _-o_ and _output/path_.

    sudo apt install p7zip-full

The __curl__ and __wget__ packages install utilities to download files from remote computers via URLs. I believe, __wget__ comes pre-installed to Ubuntu, but __curl__ doesn't. I usually use both of them.

    sudo apt install curl wget

The __ffmpeg__ package installs utility which allows me to manipulate media files like video MPEG-4, or audio MP3. But, I have found that the best option is to install __ffmpeg__ inside a __conda__ environment because it keeps the system-level environment clean.

    sudo apt install ffmpeg

The __git__ package is the most common source code management tool out there in the wild. Although, there are other tools like Plastic SCM, Perforce, etc., to work with the rest of the team, we use __git__.

    sudo apt install git git-lfs

Sometimes, Git Large-File-Storage creates issues, I resolve them by using this command:

    git lfs install --skip-repo

---

#### Xubuntu
Xubuntu (/zʊˈbʊntuː/) is a Canonical-recognized, community-maintained derivative of the Ubuntu operating system. The name Xubuntu is a portmanteau of Xfce and Ubuntu, as it uses the Xfce desktop environment, instead of Ubuntu's customized GNOME desktop.

1. **Xubuntu 20.04.1**

    The development machines were installed with the 20.04.1 version, which is an Ubuntu-based operating system with the Xfce desktop environment instead of GNOME. However, it has many minor differences which in-turn slow down the development because the target machines are pre-installed with Ubuntu. As a result, I decided to withdraw Xubuntu and concentrate solely on Ubuntu.

---

#### Arch Linux
Arch Linux (/ɑːrtʃ/) is an independently developed x86-64 general-purpose Linux distribution that strives to provide the latest stable versions of most software by following a rolling-release model. The default installation is intentionally minimal so that users can add only the packages they require.

1. **Arch Linux 2024.01.01**

    Planned to do a KVM/QEMU/libvirtio with GPU pass through for Windows and Ubuntu where I would apply Docker. Performed initial installation steps, able to setup a bootable OS.

    * archlinux/var/lib/iwd/eduroam.8021x - a configuration for eduroam network using login and password. Required by iwctl.
    * archlinux/etc/iwd/main.conf - a configuration to let built-in DHCP daemon resolve an IP address for the host.
    * Install iwd and dhcpdc and systemctl enable iwd and systemctl enable dhcpdc to run the daemons on the system start up. These tools allow me to connect to a WiFi network.

    Useful links:
    * Link to DWM installation on Arch Linux:
      * https://davidtsadler.com/posts/arch/2020-08-17/installing-st-dmenu-dwm-in-arch-linux/

### Section 1.3 Apple Operating Systems
TBD