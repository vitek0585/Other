****************************************************************************

                  Алгоритм размещения WCF Service в Service NT.

****************************************************************************

Запустить MS Visual Studio. Перейти: File - New - Project
В диалоге выбора проекта, в дереве Installed Templates справа открыть узел Visual C#
в развернувшемся узле выбрать Windows (в центре диалога отобразится список проектов)
Далее в этом списке выбрать проект - Windows Service
(ОК!)

----------------------------------------------------------------------------

В результате откроется проект, и в Solution Explorer отобразятся:

- папка References 
- файл Program.cs
- и компонент Service1.cs

----------------------------------------------------------------------------

Добавить в папку References - ServiceModel

Открыть модуль Service1.cs 
Добавить - using System.ServiceModel;
Создать в Service1.cs - WCF контракт и сервис.
Добавить следующий класс:

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

Создать файл App.config в Solution Explorer
и привести его к следующему виду:

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

        <!-- Указание Адреса, Привязки, Контракта -->
        <endpoint address=""
                  binding="basicHttpBinding"
                  contract="WindowsServiceNT.IContract" />
      </service>
    </services>

  </system.serviceModel>
</configuration>



См. Пример

----------------------------------------------------------------------------
Требуется настроить сервис для работы под любым процессором. ->

Зайти в свойства решения (Один раз кликнуть по файлу солюшенса и Alt+Enter)

Откроется окно - Solution "NameProject" Property Pages
В этом окне, в дереве слева выбрать пункт Configuration Properties (one click)
в этом окне в правом верхнем углу нажать на кнопку Configuration Manager...
Откроется окно Configuration Manager
В открывшемся окне в верхнем правом углу открыть выпадающий список и выбрать в нем пункт <New...>
Откроется окно - New Solution Platform
В открывшемся окне в вверху в выпадающем списке уже будет выбран пункт - Any CPU
Нажать кнопку ОК
В окне подтверждения (!) нажать кнопку ОК
В окне Configuration Manager нажать кнопку Close
В окне Solution "NameProject" Property Pages нажать кнопку ОК

-----------------------------------------------------------------------------

Отбилдить солюшен. (F6)

-----------------------------------------------------------------------------
Для добавления и удаления сервиса NT потребуется создать два командных файла
в которых будет вызываться утилта InstallUtil.exe:

Создать два текстовых файла. *.txt
Переименовать их в: InstallServiceNT_86.cmd и UnInstallServiceNT_86.cmd 

Открыть InstallServiceNT_86.cmd и вставить в него строки:

C:\Windows\Microsoft.NET\Framework\v4.0.30319\installutil.exe D:\WindowsService.exe
pause


Открыть UnInstallServiceNT_86.cmd и вставить в него строки:

C:\Windows\Microsoft.NET\Framework\v4.0.30319\InstallUtil.exe /u D:\WindowsService.exe
pause

-----------------------------------------------------------------------------

ВНИМАНИЕ!!!!!!!!!!!!!!!!!!!!!
Рядом с исполняемым файлом сервиса, должен лежать файл конфигурации сервиса.

-----------------------------------------------------------------------------

Добавление сервиса NT в систему:

Выполнить InstallServiceNT_86.cmd

Открыть SCM (Панель управления - Администрирование - services)

Запустить службу.

-----------------------------------------------------------------------------


Создать клиент, и обратиться с запросом к созданному сервису.


-----------------------------------------------------------------------------

Удаление сервиса NT из системы:


Открыть SCM (Панель управления - Администрирование - services)

Остановить службу.

Выполнить UnInstallServiceNT_86.cmd

-----------------------------------------------------------------------------



