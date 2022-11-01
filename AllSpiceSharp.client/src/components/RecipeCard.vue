<template>
  <div class="col-md-3 border border-dark rounded m-2 recipe-card d-flex flex-column justify-content-between">
    <div class="d-flex justify-content-between flex-row">
      <div>
        <p class="read-bgrd recipe-text fs-5"><b>{{ recipe?.category }}</b></p>
      </div>
      <div v-if="favorited" class="read-bgrd fs-5 recipe-text selectable" @click="changeFav(recipe?.id)"><i
          class="bi bi-heart-fill"></i></div>
      <div v-else class="read-bgrd fs-5 recipe-text selectable" @click="changeFav(recipe?.id)"><i
          class="bi bi-heart"></i>
      </div>
    </div>
    <div class="row">
      <h4 class="read-bgrd recipe-text">{{ recipe?.title }}</h4>
    </div>
  </div>
</template>


<script>
import { computed } from "@vue/reactivity";
import { AppState } from "../AppState.js";
import { Recipe } from "../models/Recipe.js";
import { favoritesService } from "../services/FavoritesService.js";
import Pop from "../utils/Pop.js";

export default {
  props: {
    recipe: { type: Recipe, required: true }
  },
  setup(props) {
    return {
      coverImg: computed(() => `url(${props.recipe?.img})`),
      async changeFav(id) {
        try {
          await favoritesService.changeFav(id)
        } catch (error) {
          console.error('[(UN)FAVORITE RECIPE]', error)
          Pop.error(error.message)
        }
      },
      favorited: computed(() => AppState.favRecipes.find(f => f.recipeId == props.recipe.id))
    }
  }
}
</script>


<style lang="scss" scoped>
.recipe-card {
  height: 20rem;
  width: 20rem;
  background-image: v-bind(coverImg);
  background-size: cover;
  background-position: center;
  overflow: hidden;
  position: relative;
}

.read-bgrd {
  background: rgba(78, 81, 82, 0.2);
  border: 1px solid rgba(86, 199, 251, 0.2);
  backdrop-filter: blur(10px);
  border-radius: 3%;
  padding: 0.2rem 0 0 0.2rem;
  margin-bottom: -0.003rem;
}

.recipe-text {
  color: #eaeaea;
  text-shadow: 0px 0px 4px rgba(44, 44, 44, 0.616);
  letter-spacing: 0.05em;
}
</style>