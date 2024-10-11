import axios from "axios";

export const fetchUnits = async () =>{
    try{
        var response = await axios.get("http://localhost:5285/api/ControllerB");
    return response.data;
    } catch(e)
    {
        console.error(e);
    }
};
