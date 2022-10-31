<template>
  <div class="col-md-3 border border-dark rounded m-2">
    <div class="d-flex justify-content-between flex-row">
      <div>
        <p><b>{{ recipe?.category }}</b></p>
      </div>
      <div v-if="recipe?.favorited"><i class="bi bi-heart-fill"></i></div>
      <div v-else><i class="bi bi-heart"></i></div>
    </div>
    <div class="row">
      <h5>{{ recipe?.title }}</h5>
    </div>
  </div>
</template>


<script>
import { Recipe } from "../models/Recipe.js";

export default {
  props: {
    recipe: { type: Recipe, required: true }
  },
  setup(props) {
    return {}
  }
}
</script>


<style lang="scss" scoped>

</style>