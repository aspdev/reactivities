import React from 'react';
import ApiService from "./ClientApi/ApiService";
import { EmployeesList } from "./Components/EmployeeList";


export default function App() {

    const apiService = new ApiService();
    const props = {
      getEmployees: apiService.getEmployees
    } as EmployeesList["props"]

    return (
      <EmployeesList {...props}/>
    )
}