import React, { Component } from 'react';
import { IEmployee } from "../Dtos/Employee";
import { List } from "semantic-ui-react";

interface IState {
    employees: Array<IEmployee>
}

interface DispatchProps {
    getEmployees: () => Promise<Array<IEmployee>>
}

type Props = DispatchProps

export class EmployeesList extends Component<Props, IState> {

    state = {
        employees: [] as IEmployee[]
    }

    async componentDidMount() {
        const employees = await this.props.getEmployees();
        this.setState({
            employees
        })
    }

    render () {

        const listItems = this.state.employees.map(employee => (
            <List.Item key={employee.id}>{employee.name}</List.Item>
            ))
        
            return (
                <List>
                    { listItems }
                </List>
            )
    }

   
}
