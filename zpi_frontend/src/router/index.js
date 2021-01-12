import { createWebHistory, createRouter } from "vue-router";
import HelloWorld from "@/components/HelloWorld.vue";
import MainPage from "@/components/MainPage.vue";

const routes = [
    {
        path: "/agon",
        name: "Agon",
        component: MainPage,
    },
    {
        path: "/",
        name: "Home",
        component: HelloWorld,
    },
    
];

const router = createRouter({
    history: createWebHistory(),
    routes,
});

export default router;