import axios from "axios";
import router from "@/router";

export default {
    namespaced: true,
    state: {
        teamsList: []
    },
    mutations: {
        setTeamsList(state, payload) {
            state.teamsList = payload
        }
    },
    actions: {
        getTeamsList({commit, state}) {
            axios.get('/api/teams',{
                headers:{
                    authorization: 'Bearer ' + localStorage.getItem('tokennn')
                }
            })
                .then(response => {
                    if (response.status === 200) {
                        console.log(response.data)
                        commit('setTeamsList', response.data)
                    }
                })
        },
        saveTeam({commit}, data) {
            axios.post('/api/teams', {
                Team: {
                    Name: data.teamText,
                    Description: data.descriptionText
                },
                StudentId: 1,
                TopicId: data.topicId
            },{
                headers:{
                    authorization: 'Bearer ' + localStorage.getItem('tokennn')
                }
            })
                .then(response => {
                    console.log(response)
                    router.push('/teams').then()
                })
        },

    }
}