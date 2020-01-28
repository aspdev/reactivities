import axios from "axios";
import { IEmployee } from "../Dtos/Employee"

const ajax = axios.create({
    baseURL: "http://localhost:5000/api/"
}) 

export default class ApiService {

    public async getEmployees() : Promise<Array<IEmployee>> {
        try {
           const response = await ajax.get("employee");
           return response.data;
        } catch (error) {
            return error;
        }
    }
}


 