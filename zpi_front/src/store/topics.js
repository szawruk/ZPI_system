import axios from "axios";
import {axiosBaseUrl} from "@/config/config";

import router from "@/router";

export default {
    namespaced: true,
    state: {
        topicsList: [
            {
                id: 1,
                name: 'temat1',
                description: "opis1",
            },
            {
                id: 2,
                name: 'temat2',
                description: "opis2",
            },
            {
                id: 3,
                name: 'temat3',
                description: "opis3",
            },
            {
                id: 4,
                name: 'temat4',
                description: "opis4",
            }]
    },
    mutations: {
        setTopicsList(state, payload) {
            state.topicsList = payload
        }
    },
    actions: {
        getTopicsList({commit}) {
            axios.get('/api/topics',{
                headers:{
                    authorization: 'Bearer ' + localStorage.getItem('tokennn')
                }
            })
                .then(response => {
                    if(response.status === 200){
                        console.log(response.data)
                        commit('setTopicsList', response.data)
                    }
                })
        },
        saveTopic({commit}, data) {
            axios.post('/api/topics', {Name: data.topicText, Description: data.descriptionText},{
                headers:{
                    authorization: 'Bearer ' + localStorage.getItem('tokennn')
                }
            })
                .then(response => {
                    console.log(response)
                    router.push('/topics').then()
                })
        }
    }
}