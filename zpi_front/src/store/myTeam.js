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
        setTasksList(state, payload){
            state.tasksList = payload
        },
        setTeamUsersList(state, payload){
            state.teamUsersList = payload
        }
    },
    actions: {
        getTasks({commit, state}) {
            axios.get('/api/users/1')
                .then(response => {
                    if (response.status === 200) {
                        let teamId = response.data.teamId
                        axios.get('/api/teams/' + teamId)
                            .then(response => {
                                if (response.status === 200) {
                                    console.log(response.data)
                                    commit('setTasksList', response.data.tasks)
                                }
                            })
                    }
                })
        },
        getTeamUsers({commit, state}) {
            axios.get('/api/users/1')
                .then(response => {
                    if (response.status === 200) {
                        let teamId = response.data.teamId
                        console.log("lalalal", teamId)
                        commit('main/setUserTeamId', teamId, {root: true})
                        axios.get('/api/teams/' + teamId +'/users')
                            .then(response => {
                                if (response.status === 200) {
                                    console.log(response.data.students)
                                    commit('setTeamUsersList', response.data.students)
                                }
                            })
                    }
                })
        },
        saveTask({commit, rootState}, data) {
            axios.post('/api/ProjectTasks', {
                ProjectTask: {
                    Name: data.taskText,
                    Description: data.descriptionText,
                    Deadline: data.deadline,
                    Finished: data.taskDone
                },
                StudentId: data.userId,
                TeamId: rootState.main.userTeamId
            })
                .then(response => {
                    console.log(response)
                    router.push('/my-team').then()
                })
        },
    }
}