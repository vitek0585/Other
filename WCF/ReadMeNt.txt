****************************************************************************

                  �������� ���������� WCF Service � Service NT.

****************************************************************************

��������� MS Visual Studio. �������: File - New - Project
� ������� ������ �������, � ������ Installed Templates ������ ������� ���� Visual C#
� �������������� ���� ������� Windows (� ������ ������� ����������� ������ ��������)
����� � ���� ������ ������� ������ - Windows Service
(��!)

----------------------------------------------------------------------------

� ���������� ��������� ������, � � Solution Explorer �����������:

- ����� References 
- ���� Program.cs
- � ��������� Service1.cs

----------------------------------------------------------------------------

�������� � ����� References - ServiceModel

������� ������ Service1.cs 
�������� - using System.ServiceModel;
������� � Service1.cs - WCF �������� � ������.
�������� ��������� �����:

    [RunInstaller(true)]
    public class ProjectInstaller : Installer
    {
        private ServiceProcessInstaller process;
        private ServiceInstaller service;

        public ProjectInstaller()
        {
            process = new ServiceProcessInstaller();
            process.Account = ServiceAccount.LocalSystem;

            service = new ServiceInstaller();
            service.ServiceName = "lllll ServiceNT lllll";
            service.Description = "My Service!";
            service.StartType = ServiceStartMode.Automatic;
            
            Installers.Add(process);
            Installers.Add(service);
        }
    }

������� ���� App.config � Solution Explorer
� �������� ��� � ���������� ����:

<?xml version="1.0" encoding="utf-8" ?>
<configuration>
  <system.serviceModel>

    <services>
      <service name ="WindowsServiceNT.Service">

        <host>
          <baseAddresses>
            <add baseAddress="http://localhost:4000/Service"/>
          </baseAddresses>
        </host>

        <!-- �������� ������, ��������, ��������� -->
        <endpoint address=""
                  binding="basicHttpBinding"
                  contract="WindowsServiceNT.IContract" />
      </service>
    </services>

  </system.serviceModel>
</configuration>



��. ������

----------------------------------------------------------------------------
��������� ��������� ������ ��� ������ ��� ����� �����������. ->

����� � �������� ������� (���� ��� �������� �� ����� ��������� � Alt+Enter)

��������� ���� - Solution "NameProject" Property Pages
� ���� ����, � ������ ����� ������� ����� Configuration Properties (one click)
� ���� ���� � ������ ������� ���� ������ �� ������ Configuration Manager...
��������� ���� Configuration Manager
� ����������� ���� � ������� ������ ���� ������� ���������� ������ � ������� � ��� ����� <New...>
��������� ���� - New Solution Platform
� ����������� ���� � ������ � ���������� ������ ��� ����� ������ ����� - Any CPU
������ ������ ��
� ���� ������������� (!) ������ ������ ��
� ���� Configuration Manager ������ ������ Close
� ���� Solution "NameProject" Property Pages ������ ������ ��

-----------------------------------------------------------------------------

��������� �������. (F6)

-----------------------------------------------------------------------------
��� ���������� � �������� ������� NT ����������� ������� ��� ��������� �����
� ������� ����� ���������� ������ InstallUtil.exe:

������� ��� ��������� �����. *.txt
������������� �� �: InstallServiceNT_86.cmd � UnInstallServiceNT_86.cmd 

������� InstallServiceNT_86.cmd � �������� � ���� ������:

C:\Windows\Microsoft.NET\Framework\v4.0.30319\installutil.exe D:\WindowsService.exe
pause


������� UnInstallServiceNT_86.cmd � �������� � ���� ������:

C:\Windows\Microsoft.NET\Framework\v4.0.30319\InstallUtil.exe /u D:\WindowsService.exe
pause

-----------------------------------------------------------------------------

��������!!!!!!!!!!!!!!!!!!!!!
����� � ����������� ������ �������, ������ ������ ���� ������������ �������.

-----------------------------------------------------------------------------

���������� ������� NT � �������:

��������� InstallServiceNT_86.cmd

������� SCM (������ ���������� - ����������������� - services)

��������� ������.

-----------------------------------------------------------------------------


������� ������, � ���������� � �������� � ���������� �������.


-----------------------------------------------------------------------------

�������� ������� NT �� �������:


������� SCM (������ ���������� - ����������������� - services)

���������� ������.

��������� UnInstallServiceNT_86.cmd

-----------------------------------------------------------------------------



