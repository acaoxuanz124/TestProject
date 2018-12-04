require("Define")
require("function")

require("Manager.UIManager")
require("UIDefine")


Game = {}
function Game.Start()
    UIManager.init()
    UIManager.ShowPanel(UIPanelTypes.Test)
end