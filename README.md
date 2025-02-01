# hardcode
## Table of Content
* [Introduction](#introduction)
* [Section 1. Operating Systems](#section-1-operating-systems)
* [Section 1.1. Microsoft Operating Systems](#section-11-microsoft-operating-systems)
* [Section 1.2. Linux Operating Systems](#section-12-linux-operating-systems)
* [Section 1.3. Apple Operating Systems](#section-13-apple-operating-systems)
* [Section 2. Software](#section-2-software)
* [Section 2.1 Remote Desktop](#section-21-remote-desktop)
* [Section 2.2 Virtualisation](#section-22-virtualisation)
* [Section 2.3 GPU Passthrough](#section-23-gpu-passthrough)
* [Appdenix A](#appendix-a)

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

# Computer data storage
Computer data storage is a technology consisting of computer components and recording media that are used to retain digital data. It is a core function and fundamental component of computers.

The central processing unit (CPU) of a computer is what manipulates data by performing computations. In practice, almost all computers use a storage hierarchy  which puts fast but expensive and small storage options close to the CPU and slower but less expensive and larger options further away. Generally, the fast technologies are referred to as "memory", while slower persistent technologies are referred to as "storage".

Even the first computer designs, Charles Babbage's Analytical Engine and Percy Ludgate's Analytical Machine, clearly distinguished between processing and memory (Babbage stored numbers as rotations of gears, while Ludgate stored numbers as displacements of rods in shuttles). This distinction was extended in the Von Neumann architecture, where the CPU consists of two main parts: The control unit and the arithmetic logic unit (ALU). The former controls the flow of data between the CPU and memory, while the latter performs arithmetic and logical operations on data.

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

<p align="center">
  <img src="https://media.tenor.com/CG3LSeaP8IcAAAAM/just-a-moment-loading.gif" />
</p>

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

1. **Ubuntu 24.04.1**

    The *gdm3* display manager by default runs on Wayland. However, sometimes, e.g. *ast* driver, doesn't work with Wayland. To switch to Xorg uncomment `WaylandEnable=false` line by modifying `/etc/gdm3/custom.conf` configuration file. Save the file and reboot. This is the only working solution I have found so far.

    I have tried many things, such as reinstalling by **removing** *gdm3*. I switched to *tty3*, using `Ctrl+Alt+F3` and stopped the display manager service, i.e. *gdm3*, using commands below. Stopping *gdm* and *display-manager.service* act as aliases and, in my opinion, it is better to stop the original service. I have tried both removing then rebooting, as well as, removing without rebooting. Also, I tried with and without stopping the *gdm3* service. Unfortunately, it didn't help.

        sudo systemctl stop gdm3
        sudo apt remove gdm3
        sudo apt install gdm3.

    I have tried reinstalling by **purging** *gdm3*. I did the same experiments as with **removing**. Initially, when I tried to stop the service, purge it, and install it without rebooting the display manager started to work. Happily after, I logged into my user account and all seemed to be working. However, when I have rebooted, the problem came back.

        sudo systemctl stop gdm3
        sudo apt purging gdm3
        sudo apt install gdm3.

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

## Section 2. Software

### Section 2.1 Remote Desktop
Remotely accessing desktops is a simple concept, but very cumbursome on practice. There are a lot of paid solutions such as AnyDesk, TeamViewer, ZeroTier, etc. However, ideally, we want a self-hosted solution, so that we could run it indefinitely without heavy reliance on third party developers. There we could consider open source solutions like RustDesk & RustDesk Server software, xrdp, freerdp, etc. Futhermore, we can consider software such as Guacamole. Unfortunately, all of the software has thair strong and not so strong sides. In fact, practically, I find Google Chrome Remote Desktop to be the easiest software to deal with, although, personally, I am not a big fun of it. Historically, Google Chrome Remote Desktop was the easiest to install, and when I came to my work, everyone would understand it. For example, I could say: "Oh, just use Chrome Remote Desktop", and my colleagues would get it without any further explanation. Convenient, isn't it.

Anyhow, it is time to step forward, we are acquiring new equipment, and I am going to setup a complex system with ability to give remote access to our equipment on demand whenever and wherever I am situated. For this, I have continued my experiments with various remote-desktop-software, and, here, I will document some of my findings. Starting from RustDesk. First of all, I found that for self-hosting, we need to use a pair of programs, RustDesk as a client, and RustDesk Server as a server applications. I set the server application on an AWS EC2 instance, what was relatively easy. And, following guidelines I found on the internet, I shared the ID, Relay Server, and Key with the client app. It all seemed to be working on the first day, but, on the second day, it would fail to connect to the server on my Windows machine, but wouldn't fail on my Linux machine. Also, I had to use xrdp instead of built-in gnome remote desktop on Ubuntu 24.04 because it wouldn't work the way I want it to causing me black screens.

<p align="center">
  <img src="https://media.tenor.com/GyDrUFcBvL4AAAAM/guacamole-dance.gif" />
</p>

Eventually, I stumbled upon Guacamole, following guidelines on the internet, I installed it natively on the Ubuntu 24.04. Notably, Ubuntu 24.04 doesn't provide Tomcat 9 because they moved forward to Tomcat 10. However, Guacamole doesn't like Tomcat 10, but it has not problems with Tomcat 9. To solve this problem, I had to download files directly from the Tomcat website. Luckily, installation was straight forward. The reason I didn't go with the version 10 was because Guacamole didn't support that version at that time. Not sure how it is like now. Furtermore, marrying Guacamole version 1.5.5 with gnome remote desktop was impossible. Apparently, there is a bug related with that version of Guacamole. Again, to solve this problem, I had to clone the official mirror repository of Guacamole on GitHub. After this, it seemed to be working, fingers crossed...

To install Guacamole server-side service, start by installing dependencies:

    sudo apt install build-essential libcairo2-dev libjpeg-turbo8-dev \
        libpng-dev libtool-bin libossp-uuid-dev libvncserver-dev \
        freerdp2-dev libssh2-1-dev libtelnet-dev libwebsockets-dev \
        libpulse-dev libvorbis-dev libwebp-dev libssl-dev \
        libpango1.0-dev libswscale-dev libavcodec-dev libavutil-dev \
        libavformat-dev

Then, install server-side service.

    git clone https://github.com/apache/guacamole-server
    cd guacamole-server
    autoreconf -fi
    sudo ./configure --with-init-dir=/etc/init.d --enable-allow-freerdp-snapshots
    sudo make
    sudo make install

After installation of the service, I continued to configuring the service.

    sudo ldconfig
    sudo systemctl daemon-reload
    sudo systemctl start guacd
    sudo systemctl enable guacd

As a rule of thumb, I also checked the status of the service.

    sudo systemctl status guacd

This I just followed the guide as I had no problems with creating folders at this stage either.

    sudo mkdir -p /etc/guacamole/{extensions,lib}

From now on, I focused on Tomcat 9.

    sudo apt install openjdk-17-jdk
    sudo useradd -m -U -d /opt/tomcat -s /bin/false tomcat
    sudo wget https://downloads.apache.org/tomcat/tomcat-9/v9.0.96/bin/apache-tomcat-9.0.96.tar.gz -P /tmp
    sudo tar -xvf /tmp/apache-tomcat-9.0.96.tar.gz -C /opt/tomcat
    sudo chown -R tomcat:tomcat /opt/tomcat

Tomcat is installed, we proceed to configure it.

    cd /etc/systemd/system
    sudo nvim tomcat.service

Once I created the file, I paste in the following configuration.

    [Unit]
    Description=Tomcat Server
    After=network.target

    [Service]
    Type=forking
    User=tomcat
    Group=tomcat
    Environment="JAVA_HOME=/usr/lib/jvm/java-17-openjdk-amd64"
    WorkingDirectory=/opt/tomcat/apache-tomcat-9.0.96
    ExecStart=/opt/tomcat/apache-tomcat-9.0.96/bin/startup.sh

    [Install]
    WantedBy=multi-user.target

Restarting services to let it pick up the changes won't hurt at this stage.

    sudo systemctl daemon-reload
    sudo systemctl start tomcat
    sudo systemctl enable tomcat

At this point, if I access the service at port 8080, it should render a page saying all is working. Now, I can focus my attention on Guacamole Web App client.

    sudo wget https://downloads.apache.org/guacamole/1.5.5/binary/guacamole-1.5.5.war
    sudo mv guacamole-1.5.5.war /opt/tomcat/apache-tomcat-9.0.96/webapps/guacamole.war
    sudo systemctl restart tomcat guacd

This is a ***"production-ready"*** approach, so we use a database for user authentication instead of a simple XML-file as was documented in the official manual.

    sudo apt install mariadb-server -y
    sudo mysql_secure_installation

As a note, I didn't use unix_sockets, but a regular password. The rest I left at default arguments. Next, the guide was suggesting to download an older version of a MySQL/J connector (v8.0.26), but I felt brave, and download the latest version 9.1.0. The download procedure was different for me for some reason. I had to manually download file from the website.

    sudo apt install ./mysql-connector-j_9.1.0-1ubuntu24.04_all.deb
    sudo cp /usr/share/java/mysql-connector-j-9.1.0.jar /etc/guacamole/lib/

Next, I focused on the Apache Guacamole JDBC AUTH plugin.

    sudo wget https://downloads.apache.org/guacamole/1.5.5/binary/guacamole-auth-jdbc-1.5.5.tar.gz
    sudo tar -xf guacamole-auth-jdbc-1.5.5.tar.gz
    sudo mv guacamole-auth-jdbc-1.5.5/mysql/guacamole-auth-jdbc-mysql-1.5.5.jar /etc/guacamole/extensions/

Now, it is time to configure our database.

    sudo mysql -u root -p

These are the commands inside MariaDB.

    MariaDB [(none)]> CREATE DATABASE guac_db;
    MariaDB [(none)]> CREATE USER 'guac_user'@'localhost' IDENTIFIED BY 'password';
    MariaDB [(none)]> GRANT SELECT,INSERT,UPDATE,DELETE ON guac_db.* TO 'guac_user'@'localhost';
    MariaDB [(none)]> FLUSH PRIVILEGES;
    MariaDB [(none)]> EXIT;

After, we want to apply a schema.

    cd guacamole-auth-jdbc-1.5.5/mysql/schema
    cat *.sql | mysql -u root -p guac_db

Next, we should tell Guacamole how it should handle user data. We create a simple properties file.

    sudo nvim /etc/guacamole/guacamole.properties

And, populate it with the configuration below.

    # MySQL properties
    mysql-hostname: 127.0.0.1
    mysql-port: 3306
    mysql-database: guac_db
    mysql-username: guac_user
    mysql-password: password

Restart all relevant services.

    sudo systemctl restart tomcat guacd mysql

Now, the service should be accessible at port `8080/guacamole`, and default login and password should be `guacadmin`. At this point, it is strongly recommended to create a new admin user and password and delete the default credentials. To do this, from the **guacadmin profile** click on **Settings**. Under the **Edit User** section, enter your new username and password. Then, under the **Permissions** section check the all boxes. When you are done, click **Save**. Now log out from the default user and log in back to Apache Guacamole with your new user created. Then, navigate to the settings, and users tab, and **delete the guacadmin user**. That’s it, you are done. From there, you can securely access your servers and computers.

#### References:

* [Guide to install Guacamole server program](https://medium.com/@anshumaansingh10jan/unlocking-remote-access-a-comprehensive-guide-to-installing-and-configuring-apache-guacamole-on-30a4fd227fcd)
* [Guide to install Tomcat 9 on Ubuntu 24.04](https://www.fosstechnix.com/how-to-install-tomcat-on-ubuntu-24-04-lts/)
* [Here I found how to build Guacamole from GitHub repository](https://discourse.gnome.org/t/gnome-remote-desktop-rdp-problem-with-apache-guacamole/20703/4)
* [This is the mirror repository of Apache Guacamole on GitHub](https://github.com/apache/guacamole-server)
* [The website where I download MySQL/J connector](https://dev.mysql.com/downloads/connector/j/)

### Section 2.2 Virtualisation
When working with VirtualBox, to enable bridge network, it is a simple matter of changing networking adapter in the settings of the progam to bridge adapter with the real ethernet adapter selected below. However, VirtualBox doesn't let us do hardware passthrough or, at least, I didn't find a definite answer to this on the internet. Otherwise, VirtualBox is a very useful piece of software that I use from time to time to experiment with various OS and new software.

Moving to QEMU/KVM, to allow virtual machines to connect to outside world the host's network should be reconfigured. To install QEMU/KVM with Virtual Manager to the following:

    sudo apt install qemu-kvm libvirt-daemon libvirt-daemon-system bridge-utils virt-manager

Usually, on Ubuntu 24.04.1, user is already part of `libvirt`, so we just need to add the user to the `kvm` group.

    sudo adduser $USER kvm

Once the user is added to the groups, I would suggest rebooting. After reboot, we can check if the installation is correct by checking if there are any virtual machines on the system.

    sudo virsh list --all

Or, we can check status of the `libvirt` daemon.

    sudo systemctl status libvirtd

Then, we can create bridge interface. Although, the default *NAT* allows VMs to communicate with the host and each other. And, through port forwarding on the host, we can expose the VMs to the outside world.

    nmcli con show
    nmcli con add ifname br0 type bridge con-name br0
    nmcli con add type bridge-slave ifname eth0 master br0
    nmcli con mod br0 bridge.stp no
    nmcli con down eth0
    nmcli con up br0
    nmcli device show
    sudo systemctl restart NetworkManager.service

Although, creating bridges seems to be the most logical approach, sometimes, we can't use them. Therefore, port forwarding seems to be a viable alternative. Here is how to forward ports from host physical interface to virtual interface where the guest VMs reside.

    iptables -t nat -I PREROUTING -p tcp -d HOST_IP --dport HOST_PORT -j DNAT --to-destination GUEST_IP:GUEST_PORT
    iptables -I FORWARD -m state -d GUEST_IP/24 --state NEW,RELATED,ESTABLISHED -j ACCEPT

For now, I am using `iptables-persistent` to save/reload port forwarding rules between reboots.

    sudo apt install iptables-persistent

The location is saves the rules in is `/etc/iptables/rules.v4` and `/etc/iptables/rules.v6`. To save the rules and load them, I use the following commands. I had to wrap it around as a command for a bash because I need elevated privilages on both piping as well as saving commands.

    sudo bash -c "iptables-save > /etc/iptables/rules.v4"

To setup static IP inside guest VM, we need to modify the *netplan* configuration file for the *NetworkManager* service.

    network:
        version: 2
        renderer: NetworkManager
        ethernets:
            INTERFACE_NAME:
                dhcp4: false
                addresses:
                    - STATIC_IP/24
                routes:
                    - to: default
                     via: HOST_IP
                nameservers:
                    addresses: [HOST_IP]

For example, `INTERFACE_NAME=eth0`, `STATIC_IP=192.168.1.100`, `HOST_IP=192.168.1.1`. Save the aforementioned in the `/etc/netplan/01-network-manager-all.yaml` file. And, apply the plan.

    sudo chmod 700 /etc/netplan/01-network-manager-all.yaml
    sudo netplan try

Then, we setup KVM to allow guest VMs to use bridge interface. Start from creating a file in an arbitrary location on the computer and name it `host-birdge.xml`.

    <network>
        <name>host-bridge</name>
        <forward mode="bridge"/>
        <bridge name="br0"/>
    </network>

Then execute these commands (as a user in the libvirt group):

    virsh net-define host-bridge.xml
    virsh net-start host-bridge
    virsh net-autostart host-bridge

A mechanism to allow connections from outside.

    sudo modprobe br_netfilter
    sudo echo "br_netfilter" > /etc/modules-load.d/br_netfilter.conf

Create /etc/sysctl.d/10-bridge.conf.

    # Do not filter packets crossing a bridge
    net.bridge.bridge-nf-call-ip6tables=0
    net.bridge.bridge-nf-call-iptables=0
    net.bridge.bridge-nf-call-arptables=0

Apply and check the config.

    sudo sysctl -p /etc/sysctl.d/10-bridge.conf
    sudo sysctl -a | grep "bridge-nf-call"

Configure the guest to use host-bridge. Open up the Virtual Machine Manager and then select the target guest. Go to the NIC device. The drop down for "Network Source" should now include a device called "Virtual netowrk 'host-bridge'". The "Bridge network device model" will be "virtio" if that's your KVM configuration's default. Select that "host-bridge" device.

#### Useful commands:

A command to set automatic start of VMs when host boots up.

    virsh autostart <vm-name>

A command to undo automatic start of VMs when host boots up.

    virsh autostart --disable <vm-name>

A command to set automatic start of the Docker containers when host boots up.

    docker update --restart unless-stopped <container-name>

A command to undo automatic start of the Docker containers when host boots up.

    docker update --restart no <container_name_or_id>

#### Alternative to `iptables` could be `nginx`

It is possible to configure `nginx` as a reverse proxy server on a host machine. That way, all traffic could be forwarded to an appropriate guest machine. Furthermore, using OpenResty with Lua, it is possible to configure it to pull forwarding rules from a database like MySQL, PostgreSQL, etc. Building on top, a FastAPI server would update the rules via a simple REST API endpoint. At the end, a user-oriented web application could be developed using Rest.js, Vue.js, or AngularJS. That would constitute a fully automated user-friendly system for host-guest VM management.

#### References:
* [Guide to add a bridge interface to the Ubuntu desktop using *nmcli*](https://gist.github.com/plembo/f7abd2d9b6f76e7afdece02dae7e5097)
* [Another guide to add a bridge interface that helped to understand how to set dynamic IP instead of static](https://gist.github.com/frjaraur/7bad30cedc484486efd3ba5d12362bec)
* [Guide to setup a bridged network for KVM guests](https://gist.github.com/plembo/a7b69f92953a76ab2d06533754b5e2bb)
* [Another useful guide, this is where I started my research from](https://www.dzombak.com/blog/2024/02/Setting-up-KVM-virtual-machines-using-a-bridged-network.html)
* [1/3 of QEMU/KVM Ubuntu 24.04 installation instructions I used](https://absprog.com/post/qemu-kvm-ubuntu-24-04)
* [2/3 of QEMU/KVM Ubuntu 24.04 installation instructions I used](https://www.server-world.info/en/note?os=Ubuntu_24.04&p=kvm&f=1)
* [3/3 of QEMU/KVM Ubuntu 24.04 installation instructions I used](https://phoenixnap.com/kb/ubuntu-install-kvm)
* [1/3 of port forwarding instructions I used](https://askubuntu.com/questions/720207/port-forwarding-between-bridged-interfaces)
* [2/3 of port forwarding instructions I used](https://ubuntuforums.org/showthread.php?t=2261173&p=13210545#post13210545)
* [3/3 of port forwarding instructions I used](https://serverfault.com/questions/913246/forward-traffic-from-port-22-to-virtual-computer-on-ubuntu)
* [1/2 of setting static IP using netplan and NetworkManager](https://www.freecodecamp.org/news/setting-a-static-ip-in-ubuntu-linux-ip-address-tutorial/)
* [2/2 of setting static IP using netplan and NetworkManager](https://linuxconfig.org/setting-a-static-ip-address-in-ubuntu-24-04-via-the-command-line)

### Section 2.3 GPU Passthrough

I decided to make it into a separate section because this task is complex enough on its own.

The following command can tell what kernel driver is in use.

    lspci | grep ' VGA ' | cut -d" " -f 1 | xargs -i lspci -v -s {}

The output should look something like this.

    bd:00.0 VGA compatible controller: NVIDIA Corporation GA102GL [RTX A6000] (rev a1) (prog-if 00 [VGA controller])
        Subsystem: NVIDIA Corporation GA102GL [RTX A6000]
        Physical Slot: 7
        Flags: bus master, fast devsel, latency 0, IRQ 439, NUMA node 1, IOMMU group 30
        Memory at e9000000 (32-bit, non-prefetchable) [size=16M]
        Memory at 22bfe0000000 (64-bit, prefetchable) [size=256M]
        Memory at 22bff0000000 (64-bit, prefetchable) [size=32M]
        I/O ports at d000 [size=128]
        Expansion ROM at ea000000 [virtual] [disabled] [size=512K]
        Capabilities: <access denied>
        Kernel driver in use: nvidia
        Kernel modules: nvidiafb, nouveau, nvidia_drm, nvidia

For a successful GPU passthrough, I installed Ubuntu 24.04.1 without NVIDIA drivers using only *nouveau*.

Modify *grub*.

    sudo nvim /etc/default/grub

Make it look like this.

    GRUB_CMDLINE_LINUX_DEFAULT="quiet splash intel_iommu=on iommu=pt"

Save the changes, and update grub to apply the changes. Following with a reboot.

    sudo update-grub

Blacklist the GPUs by creating a configuration file.

    sudo nvim /etc/modprobe.d/gpu-passthrough-blacklist.conf

Make it look like this.

    blacklist nouveau
    blacklist snd_hda_intel

Bind GPUs to VFIO by creating another configuration file.

    sudo nvim /etc/modprobe.d/vfio.conf

Make it look like this.

    options vfio-pci ids=XXXX:XXXX,YYYY:YYYY

`XXXX:XXXX,YYYY:YYYY` are model ids found by using the `lspci -nnk | grep -e NVIDIA` command. The ids are located at the end of the line. Shortly after, save and apply the changes.

    sudo update-initramfs -u

#### Example

Installing Ubuntu 24.04.1 from a USB flash drive. A standard procedure, however, on the step where it asks to isntall third-party drivers, we leave the checkbox empty. The "Download and install support for additional media formats" was greyed out because the machine didn't have access to the internet. The goal is to ensure that the GPUs wouldn't be used by the host system but rather on-demand by guest VMs.

The system has failed to boot on the first try. Rebooted, but the screen was black. Switched to tty3 and enabled Xorg suppport in the `gdm3` configuration file. Rebooted and logged in. The welcome page has been through, and connection to the internet was done via Wi-Fi.

Given the access to the WWW, the system was updated and upgraded. Notebly, the connection was very slow, i.e. it took around 30 minutes to complete the task. It prompted me to restart, we went with "Restart later" and then manually restarted. Enabled RDP and SSH.

Followed instructions to perform [initial configuration](#section-12-linux-operating-systems). Skipped `ffmpeg`, `fonts-powerline`, `gstreamer`, and `wget`. Proceeded with [the Virtualisation instructions](#section-22-virtualisation).

    gpu0/gpuall SSH will be available at 192.168.122.100:221.
                RDP will be available at 192.168.122.100:33891.

    gpu1/gpuduo1 SSH will be available at 192.168.122.101:221.
                 RDP will be available at 192.168.122.101:33891.

    gpu2/gputrio SSH will be available at 192.168.122.102:221.
                 RDP will be available at 192.168.122.102:33891.

    gpu3/gpuduo2 SSH will be available at 192.168.122.103:221.
                 RDP will be available at 192.168.122.103:33891.

    fileserver SSH will be available at 192.168.122.104:221.
               RDP will be available at 192.168.122.104:33891.
               MinIO will be available at 192.168.122.104:9000.
               MinIO Dashboard will be available at 192.168.122.104:9001.

#### References:
* [A guide to setup GPU passthrough](https://github.com/bryansteiner/gpu-passthrough-tutorial)
* [A guide to setup GPU passthrough](https://github.com/aaronanderson/LinuxVMWindowsSteamVR)
* [A guide to setup GPU passthrough](https://github.com/cosminmocan/vfio-single-amdgpu-passthrough)
* [How to lspci to check GPU kernel driver in use](https://askubuntu.com/questions/5417/how-to-get-the-gpu-info)
* [The comment in this link was intriguing](https://askubuntu.com/questions/1523096/single-gpu-passthrough-on-24-04-nothing-happens-when-starting-vm)
* [1/2 of the guide the comment was referring to](https://gitlab.com/risingprismtv/single-gpu-passthrough/-/wikis/home)
* [2/2 of the guide the comment was referring to](https://github.com/ilayna/Single-GPU-passthrough-amd-nvidia?tab=readme-ov-file)


### Section 2.4 Software RAID

From the internet search, the suggested tool is `mdadm`.

To install `mdadm`, run the following command in terminal.

    sudo apt install mdadm

The following command will create the RAID 5 array.

    sudo mdadm --create --verbose /dev/md0 --level=5 --raid-devices=3 /dev/sda /dev/sdb /dev/sdc

Format the RAID array and make it persistent.

    clear; sudo mkdir -p /mnt/md0
    sudo mount /dev/md0 /mnt/md0/
    df -h -x devtmpfs -x tmpfs

Then, inside the host machine, we can setup something like this.

    sudo mdadm --detail --scan | sudo tee -a /etc/mdadm/mdadm.conf
    sudo update-initramfs -u
    sudo echo '/dev/md0 /box ext4 defaults,nofail,discard 0 0' | sudo tee -a /etc/fstab

#### References:
* [The 1/2 guide](https://medium.com/@jing.kang99/create-raid-5-arrays-with-mdadm-on-ubuntu-22-04-fbb08898e01c)
* [The 2/2 guide](https://ioflood.com/blog/install-mdadm-command-linux/)

### Section 2.5 BIOS RAID

Set the SATA from `AHCI` to `RAID`.

Created volume under `Advanced/Intel(R) VROC sSATA Controller/Create RAID Volume`.

Unfortunately, my configuration doesn't support `Intel(R) VROC sSATA Controller`. I will have to use software RAID.

    CPU: Intel(R) Xeon(R) Silver 4410Y
    Generation: 4th "Sapphire Rapids"
    Chipset: Intel® C621A Chipset

#### References:
* [Intel® Virtual RAID on CPU (Intel® VROC) Operating Systems Support List](https://www.intel.com/content/www/us/en/support/articles/000099710/memory-and-storage/datacenter-storage-solutions.html)
* [Release Notes Intel® Virtual RAID on CPU (Intel® VROC) for Linux*](https://cdrdv2-public.intel.com/841093/intel-vroc-linux-releasenotes.pdf)
* [Intel® Xeon® Silver 4410Y Processor](https://www.intel.com/content/www/us/en/products/sku/232376/intel-xeon-silver-4410y-processor-30m-cache-2-00-ghz/specifications.html)
* [Intel® Virtual RAID on CPU (Intel® VROC) Linux* Driver for Intel® Server Boards and Systems Based on Intel® 621A Chipset](https://www.intel.com/content/www/us/en/download/733029/intel-virtual-raid-on-cpu-intel-vroc-linux-driver-for-intel-server-boards-and-systems-based-on-intel-621a-chipset.html)
* [GPU SuperServer SYS-740GP-TNRT](https://www.supermicro.com/en/products/system/gpu/4u/sys-740gp-tnrt)

### Section 2.6 VM for MinIO

To setup a file storage server for research work, we will setup MinIO on Ubuntu 24.04. For this, we will run a VM using KVM/QEMU. We already set up RAID 5 on the host machine and, simply, created virtual disk using entire available space.

Waiting for Ubuntu to install inside VM...

Used `parted` command line tool to partition and format the hard drive.

Modified `fstab` to add automount option. And, used UUID instead of a path because it is more reliable.

Below are commands to run a Docker container of MinIO.

    mkdir -p ${HOME}/minio/data

    docker run \
    -p 9000:9000 \
    -p 9001:9001 \
    --user $(id -u):$(id -g) \
    --name minio1 \
    -e "MINIO_ROOT_USER=ROOTUSER" \
    -e "MINIO_ROOT_PASSWORD=CHANGEME123" \
    -v ${HOME}/minio/data:/data \
    quay.io/minio/minio server /data --console-address ":9001"

#### References:
* [The tutorial I used to setup virtual disk in guest OS.](https://help.ubuntu.com/community/InstallingANewHardDrive)

# Appendix A

To hide the unmounted volumes, the setting is located at `Settings/Ubuntu Desktop/Dock/Configure Dock Behavior/Include Unmounted Volumes`.

## Command to recursively delete files.

    find . -type f -name '*.o' -exec rm {} +
    
## Command to recusrively clone a repository.

    git clone --recurse-submodules git://github.com/foo/bar.git

## Command to kill Ngrok process.

    kill -9 "$(pgrep ngrok)"

## Commands to run Ngrok in the background.
    
    clear ; ngrok http http://localhost:8080 > /dev/null &
    clear ; export WEBHOOK_URL="$(curl http://localhost:4040/api/tunnels | jq ".tunnels[0].public_url")"
    clear ; echo $WEBHOOK_URL

## Commands to install and setup Ngrok. First, sign up (in) and retrieve the authorisation token.

    snap install ngrok
    ngrok config add-authtoken <token>

## Another installation command, because when I installed **ngrok** through **snap**, it couldn't start a service, but when installed through **apt**, it worked.

    curl -s https://ngrok-agent.s3.amazonaws.com/ngrok.asc | \
      sudo gpg --dearmor -o /etc/apt/keyrings/ngrok.gpg && \
      echo "deb [signed-by=/etc/apt/keyrings/ngrok.gpg] https://ngrok-agent.s3.amazonaws.com buster main" | \
      sudo tee /etc/apt/sources.list.d/ngrok.list && \
      sudo apt update && sudo apt install ngrok
    sudo ngrok service install --config /path/to/config.yml
    sudo ngrok service start

Although, all the messages were indicating "ok", it didn't work for me. Here is the config file.
    
    authtoken: <your-auth-token>
    tunnels:
        default:
            proto: http
            addr: 8080
