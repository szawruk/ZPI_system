import axios from "axios";
import router from "@/router";

export default {
    namespaced: true,
    state: {
        selectedMenuOptions: 1,
        tasksList: [],
        teamUsersList: []
    },
    mutations: {
        setSelectedMenuOptions(state, payload) {
            state.selectedMenuOptions = payload
        },
        setTasksList(state, payload) {
            state.tasksList = payload
        },
        setTeamUsersList(state, payload) {
            state.teamUsersList = payload
        }
    },
    actions: {
        getTasks({commit, state}) {
            axios.get('/api/ProjectTasks/myTeam',
                {
                    headers: {
                        authorization: 'Bearer ' + localStorage.getItem('tokennn')
                    }
                })
                .then(response => {
                    if (response.status === 200) {
                        console.log(response)
                        commit('setTasksList', response.data)
                    }
                })
        },
        getTeamUsers({commit, state}) {
            axios.get('/api/Teams/myTeam/users',{
                headers: {
                    authorization: 'Bearer ' + localStorage.getItem('tokennn')
                }
            })
                .then(response => {
                    if (response.status === 200) {
                        console.log(response)
                        commit('setTeamUsersList', response.data.students)
                    }
                })
        },
        saveTask({commit, rootState}, data) {
            axios.post('/api/ProjectTasks', {
                ProjectTask:{
                    Name: data.taskText,
                    Description: data.descriptionText,
                    Deadline: data.deadline,
                    Finished: data.taskDone
                },
                StudentId: data.userId
            }, {
                headers: {
                    authorization: 'Bearer ' + localStorage.getItem('tokennn')
                }
            })
                .then(response => {
                    console.log(response)
                    router.push('/my-team').then()
                })
        },
    }
}