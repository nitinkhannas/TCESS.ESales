<%@ Application Language="C#" %>
<%@ Import Namespace="System" %>
<%@ Import Namespace="TCESS.ESales.CommonLayer.Mapper" %>
<%@ Import Namespace="TCESS.ESales.CommonLayer.Unity" %>
<%@ Import Namespace="TCESS.ESales.CommonLayer.UnityExtension" %>
<%@ Import Namespace="Microsoft.Practices.Unity" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e) 
    {
        //Initializes unity container and registers interface with service classes 
        ESalesUnityContainerExtension.InitializeContainer();
        
        //Creates mapping between Data transfer objects and persistence layer
        ESalesUnityContainer.Container.Resolve<IMapObject>().CreateMap();
    }
    
    void Application_End(object sender, EventArgs e)
    {
        //  Code that runs on application shutdown

    }
        
    void Application_Error(object sender, EventArgs e)
    { 
        // Code that runs when an unhandled error occurs

    }

    void Session_Start(object sender, EventArgs e)
    {
        // Code that runs when a new session is started

    }

    void Session_End(object sender, EventArgs e)
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }
       
</script>