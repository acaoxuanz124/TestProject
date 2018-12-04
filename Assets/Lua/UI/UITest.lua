---
--- Created by GM.
--- DateTime: 2018/12/3 16:33
---
local UITest = class(require("UIPanel"))

function UITest:Init()
    self.base.Init(self)
end
function UITest:Show()
    self.base.Show(self)
end
function UITest:Hide()
    self.base.Hide(self)
end
function UITest:Release()
    self.base.Release(self)
end
return UITest