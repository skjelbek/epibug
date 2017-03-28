# Demonstration of bug in EPiServer

This is a demonstration of a bug when customizing the content type icons in Episerver.

## Demo
Before commit "Customized icon for FormBlock":

![Before](https://raw.githubusercontent.com/skjelbek/epibug/master/Before.png)

After the commit:

![After](https://raw.githubusercontent.com/skjelbek/epibug/master/After.png)

## The problem
We are customizing the page- and block icons by setting the IconClass property in the UIDescriptors of the content types. This works well in both the page tree and the assets folders. However, when an item is part of a contentarea, the icon doesn’t show. That is: no icon is displayed at all. I have inspected the DOM element, and it seems like a bug in Episerver.

In the assets folder (where it works), the icon is a `<span>`-element with the classes `dijitInline dijitIcon customContentTypeIcon customContentTypeIcon--form epi-objectIcon` (our custom classes are `customContentTypeIcon customContentTypeIcon--form`. In the contentarea, the icon is an `<img>` tag with the classes `dijitIcon dijitTreeIcon dijitLeaf <span class="dijitInline customContentTypeIcon customContentTypeIcon--form epi-objectIcon"></span>`. As you can see, an html element has snuck inside the class attribute.

I think the reason why this hasn’t been reported earlier is because this actually is a valid value for the class attribute, and no one notices it as long as one is setting the icon on the element `<img>` element itself (e.g. through a background sprite). In our case, we use an `:after` selector to set a custom icon from the material icons font. `:after` pseudo elements doesn’t work on self-closing tags like `<img>`.
