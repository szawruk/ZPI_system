import VueRouter from 'vue-router'
import MainPage from "@/components/MainPage.vue";
import Teams from "@/components/Teams";
import Topics from "@/components/Topics";
import CreateTopic from "@/components/CreateTopic";
import CreateTeam from "@/components/CreateTeam";
import Vue from 'vue'

Vue.use(VueRouter)

const routes = [
    {
        path: "/",
        name: "Home",
        component: MainPage,
    },
    {
        path: "/topics/new",
        name: "CreateTopic",
        component: CreateTopic,
    },
    {
        path: "/teams/new",
        name: "CreateTeam",
        component: CreateTeam,
    },
    {
        path: "/teams",
        name: "Teams",
        component: Teams,
    },
    {
        path: "/topics",
        name: "Topics",
        component: Topics,
    },


    
];

const router = new VueRouter({
    routes,
})

export default router;