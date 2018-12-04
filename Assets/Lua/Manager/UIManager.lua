---
--- Created by GM.
--- DateTime: 2018/11/30 15:51
---



UILayerType = {
    Panel = 1,
    Tip = 2,
}
UIManager = {}
local self = UIManager

local width = 1334
local height = 750
function UIManager.init()

end
function UIManager.GetPanel(panelName)
    if panelName == nil then
        return
    end
    local panel = self.uiPanels[panelName]
    return panel
end
function UIManager.ShowPanel(panelType,callBack)

end
