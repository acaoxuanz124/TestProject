---
--- Created by GM.
--- DateTime: 2018/12/3 15:03
---
local UIPanel = class("UIPanel")

function UIPanel:ctor()
    
end
function UIPanel:Awake(uiGobj)
    self.view = uiGobj
    self:Init()
end
function UIPanel:Init()
    
end

function UIPanel:Show()

end
function UIPanel:Hide()

end

function UIPanel:Release()

end
return UIPanel


