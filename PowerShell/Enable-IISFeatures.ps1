# Enable IIS on a box.
# TODO - Add standard structure - Help, Logging, Error handling
# [ ] Web Server (IIS)                                    Web-Server             
#     [ ] Web Server                                      Web-WebServer          
#         [ ] Common HTTP Features                        Web-Common-Http        
#             [ ] Static Content                          Web-Static-Content     
#             [ ] Default Document                        Web-Default-Doc        
#             [ ] Directory Browsing                      Web-Dir-Browsing       
#             [ ] HTTP Errors                             Web-Http-Errors        
#             [ ] HTTP Redirection                        Web-Http-Redirect      
#             [ ] WebDAV Publishing                       Web-DAV-Publishing     
#         [ ] Application Development                     Web-App-Dev            
#             [ ] ASP.NET                                 Web-Asp-Net            
#             [ ] .NET Extensibility                      Web-Net-Ext            
#             [ ] ASP                                     Web-ASP                
#             [ ] CGI                                     Web-CGI                
#             [ ] ISAPI Extensions                        Web-ISAPI-Ext          
#             [ ] ISAPI Filters                           Web-ISAPI-Filter       
#             [ ] Server Side Includes                    Web-Includes           
#         [ ] Health and Diagnostics                      Web-Health             
#             [ ] HTTP Logging                            Web-Http-Logging       
#             [ ] Logging Tools                           Web-Log-Libraries      
#             [ ] Request Monitor                         Web-Request-Monitor    
#             [ ] Tracing                                 Web-Http-Tracing       
#             [ ] Custom Logging                          Web-Custom-Logging     
#             [ ] ODBC Logging                            Web-ODBC-Logging       
#         [ ] Security                                    Web-Security           
#             [ ] Basic Authentication                    Web-Basic-Auth         
#             [ ] Windows Authentication                  Web-Windows-Auth       
#             [ ] Digest Authentication                   Web-Digest-Auth        
#             [ ] Client Certificate Mapping Authentic... Web-Client-Auth        
#             [ ] IIS Client Certificate Mapping Authe... Web-Cert-Auth          
#             [ ] URL Authorization                       Web-Url-Auth           
#             [ ] Request Filtering                       Web-Filtering          
#             [ ] IP and Domain Restrictions              Web-IP-Security        
#         [ ] Performance                                 Web-Performance        
#             [ ] Static Content Compression              Web-Stat-Compression   
#             [ ] Dynamic Content Compression             Web-Dyn-Compression    
#     [ ] Management Tools                                Web-Mgmt-Tools         
#         [ ] IIS Management Console                      Web-Mgmt-Console       
#         [ ] IIS Management Scripts and Tools            Web-Scripting-Tools    
#         [ ] Management Service                          Web-Mgmt-Service       
#         [ ] IIS 6 Management Compatibility              Web-Mgmt-Compat        
#             [ ] IIS 6 Metabase Compatibility            Web-Metabase           
#             [ ] IIS 6 WMI Compatibility                 Web-WMI                
#             [ ] IIS 6 Scripting Tools                   Web-Lgcy-Scripting     
#             [ ] IIS 6 Management Console                Web-Lgcy-Mgmt-Console  
#     [ ] FTP Server                                      Web-Ftp-Server         
#         [ ] FTP Service                                 Web-Ftp-Service        
#         [ ] FTP Extensibility                           Web-Ftp-Ext            
#     [ ] IIS Hostable Web Core                           Web-WHC                

Get-WindowsFeature Web*
# Common HTTP Features
Add-WindowsFeature -Name Web-Static-Content,Web-Default-Doc,Web-Dir-Browsing,Web-Http-Errors,Web-Http-Redirect
# Application Development
Add-WindowsFeature -Name Web-Asp-Net,Web-Net-Ext,Web-ISAPI-Ext,Web-ISAPI-Filter
# Health and Diagnostics
Add-WindowsFeature -Name Web-Http-Logging,Web-Log-Libraries,Web-Request-Monitor
# Security
Add-WindowsFeature -Name Web-Windows-Auth,Web-Filtering
# Performance
Add-WindowsFeature -Name Web-Stat-Compression,Web-Dyn-Compression
# Management Tools
Add-WindowsFeature -Name Web-Mgmt-Console,Web-Scripting-Tools,Web-Mgmt-Service,Web-Mgmt-Compat,Web-Metabase
Get-WindowsFeature Web*