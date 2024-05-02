"use strict";(self.webpackChunknetworkmanager=self.webpackChunknetworkmanager||[]).push([[7215],{6023:(e,o,n)=>{n.r(o),n.d(o,{assets:()=>d,contentTitle:()=>s,default:()=>c,frontMatter:()=>r,metadata:()=>a,toc:()=>h});var i=n(4848),t=n(8453);const r={sidebar_position:18},s="Whois",a={id:"application/whois",title:"Whois",description:"With Whois you can retrieve Whois information for a domain directly from the Whois server associated with the top-level domain.",source:"@site/docs/application/whois.md",sourceDirName:"application",slug:"/application/whois",permalink:"/NETworkManager/docs/application/whois",draft:!1,unlisted:!1,editUrl:"https://github.com/bornToBeRoot/NETworkManager/tree/main/Website/docs/application/whois.md",tags:[],version:"current",sidebarPosition:18,frontMatter:{sidebar_position:18},sidebar:"docsSidebar",previous:{title:"Wake on LAN",permalink:"/NETworkManager/docs/application/wake-on-lan"},next:{title:"IP Geolocation",permalink:"/NETworkManager/docs/application/ip-geolocation"}},d={},h=[{value:"Profile",id:"profile",level:2},{value:"Inherit host from general",id:"inherit-host-from-general",level:3},{value:"Domain",id:"domain",level:3}];function l(e){const o={a:"a",admonition:"admonition",code:"code",h1:"h1",h2:"h2",h3:"h3",img:"img",li:"li",p:"p",strong:"strong",ul:"ul",...(0,t.R)(),...e.components};return(0,i.jsxs)(i.Fragment,{children:[(0,i.jsx)(o.h1,{id:"whois",children:"Whois"}),"\n",(0,i.jsxs)(o.p,{children:["With ",(0,i.jsx)(o.strong,{children:"Whois"})," you can retrieve Whois information for a domain directly from the Whois server associated with the top-level domain."]}),"\n",(0,i.jsx)(o.admonition,{type:"info",children:(0,i.jsx)(o.p,{children:"Whois data from a domain is publicly available. The data is provided by the domain registrar and can be queried via the whois protocol. The whois protocol is based on TCP and uses port 43. The whois protocol is not encrypted and the data is transmitted in plain text. Because the whois protocol is not standardized, the data may have a different format depending on the registrar."})}),"\n",(0,i.jsx)(o.admonition,{type:"note",children:(0,i.jsxs)(o.p,{children:["In order to use the whois protocol, the firewall must allow outgoing connections on port 43 to the whois server associated with the top-level domain. For example, if you want to query the whois information for ",(0,i.jsx)(o.code,{children:"borntoberoot.net"}),", you must allow outgoing connections to ",(0,i.jsx)(o.code,{children:"whois.verisign-grs.com"})," on port 43."]})}),"\n",(0,i.jsx)(o.admonition,{type:"warning",children:(0,i.jsx)(o.p,{children:"For .de domains, DENIC no longer provides information via the whois protocol."})}),"\n",(0,i.jsx)(o.p,{children:"Example inputs:"}),"\n",(0,i.jsxs)(o.ul,{children:["\n",(0,i.jsx)(o.li,{children:(0,i.jsx)(o.code,{children:"borntoberoot.net"})}),"\n"]}),"\n",(0,i.jsx)(o.p,{children:(0,i.jsx)(o.img,{alt:"Whois",src:n(1596).A+"",width:"1200",height:"800"})}),"\n",(0,i.jsx)(o.admonition,{type:"note",children:(0,i.jsx)(o.p,{children:"Right-click on the result to copy or export the information."})}),"\n",(0,i.jsx)(o.h2,{id:"profile",children:"Profile"}),"\n",(0,i.jsx)(o.h3,{id:"inherit-host-from-general",children:"Inherit host from general"}),"\n",(0,i.jsx)(o.p,{children:"Inherit the host from the general settings."}),"\n",(0,i.jsxs)(o.p,{children:[(0,i.jsx)(o.strong,{children:"Type:"})," ",(0,i.jsx)(o.code,{children:"Boolean"})]}),"\n",(0,i.jsxs)(o.p,{children:[(0,i.jsx)(o.strong,{children:"Default:"})," ",(0,i.jsx)(o.code,{children:"Enabled"})]}),"\n",(0,i.jsx)(o.admonition,{type:"note",children:(0,i.jsxs)(o.p,{children:["If this option is enabled, the ",(0,i.jsx)(o.a,{href:"#domain",children:"domain"})," is overwritten by the host from the general settings and the ",(0,i.jsx)(o.a,{href:"#domain",children:"domain"})," is disabled."]})}),"\n",(0,i.jsx)(o.h3,{id:"domain",children:"Domain"}),"\n",(0,i.jsx)(o.p,{children:"Domain to query for whois information."}),"\n",(0,i.jsxs)(o.p,{children:[(0,i.jsx)(o.strong,{children:"Type:"})," ",(0,i.jsx)(o.code,{children:"String"})]}),"\n",(0,i.jsxs)(o.p,{children:[(0,i.jsx)(o.strong,{children:"Default:"})," ",(0,i.jsx)(o.code,{children:"Empty"})]}),"\n",(0,i.jsxs)(o.p,{children:[(0,i.jsx)(o.strong,{children:"Example:"})," ",(0,i.jsx)(o.code,{children:"borntoberoot.net"})]})]})}function c(e={}){const{wrapper:o}={...(0,t.R)(),...e.components};return o?(0,i.jsx)(o,{...e,children:(0,i.jsx)(l,{...e})}):l(e)}},1596:(e,o,n)=>{n.d(o,{A:()=>i});const i=n.p+"assets/images/whois-116e5d692853794cc215dc5ba196e243.png"},8453:(e,o,n)=>{n.d(o,{R:()=>s,x:()=>a});var i=n(6540);const t={},r=i.createContext(t);function s(e){const o=i.useContext(r);return i.useMemo((function(){return"function"==typeof e?e(o):{...o,...e}}),[o,e])}function a(e){let o;return o=e.disableParentContext?"function"==typeof e.components?e.components(t):e.components||t:s(e.components),i.createElement(r.Provider,{value:o},e.children)}}}]);