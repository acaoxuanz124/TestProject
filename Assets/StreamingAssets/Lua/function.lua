function handler(obj,func)
    return function(...)
        func(obj,...)
    end
end

function class(base)
    local obj={}
    obj.base=base
    obj.ctor=function()

    end
    obj.New=function( ... )
        local NewObj={}
        NewObj.base=base
        NewObj.ctor=function()

        end
        local callCtor
        callCtor = function(curClassType,...)
            if curClassType.base~=nil then
                callCtor(curClassType.base,...)
            end
            if curClassType.ctor ~= nil then
                curClassType.ctor(NewObj,...)
            end
        end
        callCtor(NewObj,...)

        setmetatable(NewObj, {__index=obj})

        return NewObj

    end
    setmetatable(obj, {__index=base})
    return obj
end
function string.IsNullOrEmpty(str)
    return str == nil or str == ""
end
function table.tostring(table)

    local tostring=nil
    tostring=function(table,tab)
        if tab==nil then
            tab=0
        end
        local str="{\n"
        for k,v in pairs(table) do
            local typeStr=type(v)
            for i=1,tab do
                str=str.."\r\t"
            end
            str=str..k..string.format("[%s]",typeStr)
            if typeStr=="string" then
                if v=="" then
                    str=str.."=\"\"\n"
                else
                    str=str.."=".."\""..v.."\"".."\n"
                end
            elseif typeStr=="number" then
                str=str.."="..v.."\n"
            elseif typeStr==nil then
                str=str.."=nil\n"
            elseif typeStr=="table" then
                str=str.."="..tostring(v,tab+1).."\n"
            end
        end
        for i=1,tab-1 do
            str=str.."\r\t"
        end
        str=str.."}"
        return str
    end
    return tostring(table,1)
end


function Log()

end

function LogError()
    
end
function LogWarning()
    
end