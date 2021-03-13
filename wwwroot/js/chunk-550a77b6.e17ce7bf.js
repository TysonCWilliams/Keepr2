(window["webpackJsonp"]=window["webpackJsonp"]||[]).push([["chunk-550a77b6"],{"1a3e":function(e,t,a){"use strict";a.r(t);var l=a("7a23");const o=Object(l["I"])("data-v-e7036912");Object(l["u"])("data-v-e7036912");const c={class:"profile text-center container-fluid"},s={class:"row"},d={class:"col-sm-1 col-lg-12 mt-3 ml-1"},i={class:"col-sm-1 col-lg-12 mt-4"},n={class:"profile-title mr-1"},r={class:"mt-1 info-vk",style:{width:"140px"}},b={class:"mt-1 info-vk",style:{width:"140px"}},p={class:"row ml-2 mt-4"},m=Object(l["h"])("h3",{class:"mt-2"}," Vaults: ",-1),u=Object(l["h"])("i",{class:"fas fa-plus-square fa-2x fa-spin"},null,-1),h={class:"col-sm-12",style:{display:"flex","flex-direction":"row","flex-wrap":"wrap"}},j={key:0,style:{display:"block !important"},class:"modal fade show",id:"form",tabindex:"-1",role:"dialog"},O={class:"modal-dialog modal-dialog-centered",role:"document"},f={class:"modal-content"},v={class:"modal-header border-bottom-0"},y=Object(l["h"])("h5",{class:"modal-title",id:"exampleModalLabel"}," Create Vault ",-1),g=Object(l["h"])("span",{"aria-hidden":"true"},"×",-1),w={class:"modal-body"},k={class:"form-group"},V=Object(l["h"])("label",{for:"vault-name"},"Name",-1),K={class:"form-group"},x=Object(l["h"])("label",{for:"vault-description"},"Description",-1),M={class:"form-check"},C=Object(l["h"])("label",{class:"form-check-label",for:"isPrivate"},"Private?",-1),E={class:"modal-footer border-top-0 d-flex justify-content-center"},P={class:"row ml-2 mt-4"},U=Object(l["h"])("h4",{class:"mt-2"}," Keeps: ",-1),B=Object(l["h"])("i",{class:"fas fa-plus-square fa-2x fa-spin"},null,-1),H={class:"modal-dialog modal-dialog-centered",role:"document"},A={class:"modal-content"},D={class:"modal-header border-bottom-0"},N=Object(l["h"])("h5",{class:"modal-title",id:"exampleModalLabel"}," Create Keep ",-1),L=Object(l["h"])("span",{"aria-hidden":"true"},"×",-1),q={class:"modal-body"},I={class:"form-group"},J=Object(l["h"])("label",{for:"keep-name"},"Title",-1),_={class:"form-group"},F=Object(l["h"])("label",{for:"keep-image"},"Image",-1),R={class:"form-group"},S=Object(l["h"])("label",{for:"keep-description"},"Description",-1),T={class:"modal-footer border-top-0 d-flex justify-content-center"},z={class:"col-sm-12",style:{display:"flex","flex-direction":"row","flex-wrap":"wrap"}};Object(l["s"])();const G=o((function(e,t,a,o,G,Q){const W=Object(l["y"])("vault-component"),X=Object(l["y"])("keep-component");return Object(l["r"])(),Object(l["e"])("div",c,[Object(l["h"])("div",s,[Object(l["h"])("div",d,[Object(l["h"])("img",{class:"rounded",src:o.profile.picture,alt:""},null,8,["src"])]),Object(l["h"])("div",i,[Object(l["h"])("h2",n,Object(l["B"])(o.profile.name),1),Object(l["h"])("h4",r," Vaults: "+Object(l["B"])(o.state.myVaults.length),1),Object(l["h"])("h4",b," Keeps: "+Object(l["B"])(o.state.myKeeps.length),1)])]),Object(l["h"])("div",p,[m,Object(l["h"])("button",{type:"button",style:{display:"block"},onClick:t[1]||(t[1]=e=>o.toggleVaultModal()),class:"btn btn-light ml-3 mb-3","data-toggle":"modal","data-target":"#form"},[u])]),Object(l["h"])("div",h,[(Object(l["r"])(!0),Object(l["e"])(l["a"],null,Object(l["x"])(o.state.myVaults,e=>(Object(l["r"])(),Object(l["e"])("div",{class:"ml-2 mr-3 mt-3",key:e.id},[Object(l["h"])(W,{style:{position:"relative","max-width":"100%"},"vault-prop":e},null,8,["vault-prop"])]))),128))]),o.state.showVaultModal?(Object(l["r"])(),Object(l["e"])("div",j,[Object(l["h"])("div",O,[Object(l["h"])("div",f,[Object(l["h"])("div",v,[y,Object(l["h"])("button",{onClick:t[2]||(t[2]=e=>o.toggleVaultModal()),type:"button",class:"close","data-dismiss":"modal","aria-label":"Close"},[g])]),Object(l["h"])("form",null,[Object(l["h"])("div",w,[Object(l["h"])("div",k,[V,Object(l["H"])(Object(l["h"])("input",{type:"text","onUpdate:modelValue":t[3]||(t[3]=e=>o.state.newVault.name=e),class:"form-control",id:"vault-name","aria-describedby":"vault-name",placeholder:"Enter Vault Name..."},null,512),[[l["E"],o.state.newVault.name]])]),Object(l["h"])("div",K,[x,Object(l["H"])(Object(l["h"])("input",{type:"text","onUpdate:modelValue":t[4]||(t[4]=e=>o.state.newVault.description=e),class:"form-control",id:"vault-description",placeholder:"Description"},null,512),[[l["E"],o.state.newVault.description]])]),Object(l["h"])("div",M,[Object(l["H"])(Object(l["h"])("input",{type:"checkbox",class:"form-check-input",id:"isPrivate","onUpdate:modelValue":t[5]||(t[5]=e=>o.state.newVault.isPrivate=e)},null,512),[[l["D"],o.state.newVault.isPrivate]]),C])]),Object(l["h"])("div",E,[Object(l["h"])("button",{type:"submit",onClick:t[6]||(t[6]=e=>(o.createVault(),o.toggleVaultModal())),class:"btn btn-success"}," Create ")])])])])])):Object(l["f"])("",!0),Object(l["h"])("div",P,[U,Object(l["h"])("button",{type:"button",style:{display:"block"},onClick:t[7]||(t[7]=e=>o.toggleKeepModal()),class:"btn btn-light ml-3 mb-3","data-toggle":"modal","data-target":"#form"},[B])]),o.state.showKeepModal?(Object(l["r"])(),Object(l["e"])("div",{key:1,style:{display:"block !important"},class:"modal fade show",id:"form",tabindex:"-1",role:"dialog","aria-labelledby":"exampleModalLabel","aria-hidden":o.state.showKeepModal},[Object(l["h"])("div",H,[Object(l["h"])("div",A,[Object(l["h"])("div",D,[N,Object(l["h"])("button",{onClick:t[8]||(t[8]=e=>o.toggleKeepModal()),type:"button",class:"close","data-dismiss":"modal","aria-label":"Close"},[L])]),Object(l["h"])("form",null,[Object(l["h"])("div",q,[Object(l["h"])("div",I,[J,Object(l["H"])(Object(l["h"])("input",{type:"text","onUpdate:modelValue":t[9]||(t[9]=e=>o.state.newKeep.name=e),class:"form-control",id:"keep-name","aria-describedby":"keep-name",placeholder:"Enter Keep Name..."},null,512),[[l["E"],o.state.newKeep.name]])]),Object(l["h"])("div",_,[F,Object(l["H"])(Object(l["h"])("input",{type:"text","onUpdate:modelValue":t[10]||(t[10]=e=>o.state.newKeep.img=e),class:"form-control",id:"keep-image",placeholder:"URL..."},null,512),[[l["E"],o.state.newKeep.img]])]),Object(l["h"])("div",R,[S,Object(l["H"])(Object(l["h"])("input",{type:"text","onUpdate:modelValue":t[11]||(t[11]=e=>o.state.newKeep.description=e),class:"form-control",id:"keep-description",placeholder:"Enter Description..."},null,512),[[l["E"],o.state.newKeep.description]])])]),Object(l["h"])("div",T,[Object(l["h"])("button",{type:"submit",onClick:t[12]||(t[12]=e=>(o.createKeep(),o.toggleKeepModal())),class:"btn btn-success"}," Create ")])])])])],8,["aria-hidden"])):Object(l["f"])("",!0),Object(l["h"])("div",z,[(Object(l["r"])(!0),Object(l["e"])(l["a"],null,Object(l["x"])(o.state.myKeeps,e=>(Object(l["r"])(),Object(l["e"])("div",{class:"ml-2 mr-3 mt-3",key:e.id},[Object(l["h"])(X,{style:{position:"relative",width:"100%"},"keep-prop":e},null,8,["keep-prop"])]))),128))])])}));var Q=a("83fc"),W=a("6c96"),X=a("b5fa"),Y=a("255a"),Z=a("d10d"),$=a("ab41"),ee={name:"Profile",setup(){Object(l["p"])(async()=>{Q["a"].profile.id||await X["a"].getProfile();const t=await W["a"].getMyKeeps();e.myKeeps=t;const a=await Z["a"].getAllVaultsForUser(Q["a"].profile.id);e.myVaults=a});const e=Object(l["v"])({myKeeps:[],myVaults:[],showKeepModal:!1,showVaultModel:!1,toggleKeepModal:!1,toggleVaultModal:!1,newKeep:{name:"",description:"",img:""},newVault:{name:"",description:"",isPrivate:!1}});return{state:e,createKeep(){W["a"].createKeep(e.newKeep)},createVault(){Z["a"].createVault(e.newVault)},profile:Object(l["c"])(()=>Q["a"].profile),keeps:Object(l["c"])(()=>Q["a"].keeps),vaults:Object(l["c"])(()=>Q["a"].vaults),toggleKeepModal(){e.showKeepModal=!e.showKeepModal,this.removeAllModalBackdrops()},toggleVaultModal(){e.showVaultModal=!e.showVaultModal,this.removeAllModalBackdrops()},removeAllModalBackdrops(){document.body.className=document.body.className.replace("modal-open",""),document.querySelectorAll(".modal-backdrop").forEach((function(e){e.remove()}))}}},components:{KeepComponent:Y["KeepComponent"],VaultComponent:$["VaultComponent"]}};a("dfdb");ee.render=G,ee.__scopeId="data-v-e7036912";t["default"]=ee},"1d46":function(e,t,a){},dfdb:function(e,t,a){"use strict";a("1d46")}}]);